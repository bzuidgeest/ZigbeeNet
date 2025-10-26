using System;
using System.Collections.Generic;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace ZigBeeNet.EmberV8Plus.CodeGenerator.Models
{
    /// <summary>
    /// Custom YAML converter for TypedefDefinition that intelligently deserializes
    /// either a string or a list of TypedefField objects
    /// </summary>
    public class TypedefDefinitionConverter : IYamlTypeConverter
    {
        public bool Accepts(Type type)
        {
            return type == typeof(TypedefDefinition);
        }

        public object ReadYaml(IParser parser, Type type, ObjectDeserializer rootDeserializer)
        {
            // Check what type of node we're dealing with
            if (parser.Current is Scalar scalar)
            {
                // It's a simple string definition
                parser.MoveNext();
                return new SimpleTypedefDefinition(scalar.Value);
            }
            else if (parser.Current is SequenceStart)
            {
                // It's a complex definition with fields
                var fields = new List<TypedefField>();
                parser.MoveNext(); // Move past SequenceStart

                while (!(parser.Current is SequenceEnd))
                {
                    if (parser.Current is MappingStart)
                    {
                        var field = DeserializeField(parser);
                        fields.Add(field);
                    }
                    else
                    {
                        parser.MoveNext();
                    }
                }

                parser.MoveNext(); // Move past SequenceEnd
                return new ComplexTypedefDefinition(fields);
            }
            else if (parser.Current is MappingStart)
            {
                // Single mapping (shouldn't happen based on YAML structure, but handle it)
                var field = DeserializeField(parser);
                return new ComplexTypedefDefinition(new List<TypedefField> { field });
            }

            throw new YamlException($"Unexpected node type: {parser.Current?.GetType().Name}");
        }

        public void WriteYaml(IEmitter emitter, object value, Type type, ObjectSerializer serializer)
        {
            if (value is SimpleTypedefDefinition simple)
            {
                emitter.Emit(new Scalar(null, null, simple.Type, ScalarStyle.Plain, true, false));
            }
            else if (value is ComplexTypedefDefinition complex)
            {
                emitter.Emit(new SequenceStart(null, null, true, SequenceStyle.Block));

                foreach (var field in complex.Fields)
                {
                    emitter.Emit(new MappingStart(null, null, true, MappingStyle.Block));

                    emitter.Emit(new Scalar(null, null, "type", ScalarStyle.Plain, true, false));
                    emitter.Emit(new Scalar(null, null, field.Type, ScalarStyle.Plain, true, false));

                    emitter.Emit(new Scalar(null, null, "name", ScalarStyle.Plain, true, false));
                    emitter.Emit(new Scalar(null, null, field.Name, ScalarStyle.Plain, true, false));

                    emitter.Emit(new Scalar(null, null, "description", ScalarStyle.Plain, true, false));
                    emitter.Emit(new Scalar(null, null, field.Description, ScalarStyle.Plain, true, false));

                    emitter.Emit(new MappingEnd());
                }

                emitter.Emit(new SequenceEnd());
            }
        }

        private TypedefField DeserializeField(IParser parser)
        {
            var field = new TypedefField();
            parser.MoveNext(); // Move past MappingStart

            while (!(parser.Current is MappingEnd))
            {
                if (parser.Current is Scalar keyScalar)
                {
                    string key = keyScalar.Value;
                    parser.MoveNext();

                    if (parser.Current is Scalar valueScalar)
                    {
                        string value = valueScalar.Value;
                        switch (key)
                        {
                            case "type":
                                field.Type = value;
                                break;
                            case "name":
                                field.Name = value;
                                break;
                            case "description":
                                field.Description = value;
                                break;
                        }
                        parser.MoveNext();
                    }
                    else
                    {
                        parser.MoveNext();
                    }
                }
                else
                {
                    parser.MoveNext();
                }
            }

            parser.MoveNext(); // Move past MappingEnd
            return field;
        }
    }
}

