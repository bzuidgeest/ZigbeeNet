#!/bin/bash

# Bash script to build and package the ASH Protocol library

set -e

# Default values
CONFIGURATION="Release"
VERSION="1.0.0"
PROJECT_PATH=""
SKIP_TESTS=false
PACK=false
PUSH=false
API_KEY=""
SOURCE="https://api.nuget.org/v3/index.json"

# Get script directory
SCRIPT_DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"

# Parse command line arguments
while [[ $# -gt 0 ]]; do
    case $1 in
        -c|--configuration)
            CONFIGURATION="$2"
            shift 2
            ;;
        -v|--version)
            VERSION="$2"
            shift 2
            ;;
        -p|--project)
            PROJECT_PATH="$2"
            shift 2
            ;;
        --skip-tests)
            SKIP_TESTS=true
            shift
            ;;
        --pack)
            PACK=true
            shift
            ;;
        --push)
            PUSH=true
            shift
            ;;
        --api-key)
            API_KEY="$2"
            shift 2
            ;;
        --source)
            SOURCE="$2"
            shift 2
            ;;
        *)
            echo "Unknown option: $1"
            exit 1
            ;;
    esac
done

echo "============================================"
echo "ASH Protocol Library - Build & Package"
echo "============================================"
echo ""

# Auto-detect project file if not specified
if [ -z "$PROJECT_PATH" ]; then
    echo "Auto-detecting project file..."
    
    # Find .csproj files in script directory
    CSPROJ_FILES=("$SCRIPT_DIR"/*.csproj)
    CSPROJ_COUNT=${#CSPROJ_FILES[@]}
    
    if [ ! -e "${CSPROJ_FILES[0]}" ]; then
        echo "Error: No .csproj file found in $SCRIPT_DIR"
        echo "Please specify project path with -p or --project parameter"
        exit 1
    elif [ $CSPROJ_COUNT -gt 1 ]; then
        echo "Error: Multiple .csproj files found:"
        for file in "${CSPROJ_FILES[@]}"; do
            echo "  - $(basename "$file")"
        done
        echo "Please specify which project with -p or --project parameter"
        exit 1
    fi
    
    PROJECT_PATH="${CSPROJ_FILES[0]}"
    echo "Found project: $(basename "$PROJECT_PATH")"
fi

# Resolve relative path
if [[ "$PROJECT_PATH" != /* ]]; then
    PROJECT_PATH="$SCRIPT_DIR/$PROJECT_PATH"
fi

# Verify project file exists
if [ ! -f "$PROJECT_PATH" ]; then
    echo "Error: Project file not found: $PROJECT_PATH"
    exit 1
fi

PROJECT_DIR="$(dirname "$PROJECT_PATH")"
PROJECT_NAME="$(basename "$PROJECT_PATH" .csproj)"

echo "Project: $PROJECT_NAME"
echo "Location: $PROJECT_DIR"
echo ""

# Change to project directory
cd "$PROJECT_DIR"

# Clean previous builds
echo "Cleaning previous builds..."
rm -rf bin obj
rm -f *.nupkg *.snupkg

# Restore dependencies
echo ""
echo "Restoring dependencies..."
dotnet restore "$PROJECT_PATH"

# Build all target frameworks
echo ""
echo "Building for all target frameworks ($CONFIGURATION)..."
dotnet build "$PROJECT_PATH" --configuration "$CONFIGURATION" --no-restore /p:Version="$VERSION"

# Run tests (if not skipped)
if [ "$SKIP_TESTS" = false ]; then
    echo ""
    echo "Running tests..."
    dotnet test "$PROJECT_PATH" --configuration "$CONFIGURATION" --no-build --verbosity normal
fi

# Create NuGet packages
if [ "$PACK" = true ]; then
    echo ""
    echo "Creating NuGet packages..."
    
    dotnet pack "$PROJECT_PATH" --configuration "$CONFIGURATION" --no-build /p:Version="$VERSION" \
        /p:IncludeSymbols=true /p:SymbolPackageFormat=snupkg
    
    echo ""
    echo "Created packages:"
    find "bin/$CONFIGURATION" -name "*.nupkg" -type f -exec basename {} \;
fi

# Push to NuGet (if requested)
if [ "$PUSH" = true ]; then
    if [ "$PACK" = false ]; then
        echo "Error: Cannot push without packing. Use --pack flag."
        exit 1
    fi
    
    if [ -z "$API_KEY" ]; then
        echo "Error: API key required for pushing. Use --api-key parameter."
        exit 1
    fi
    
    echo ""
    echo "Pushing packages to $SOURCE..."
    
    # Push main packages (excluding symbols)
    find "bin/$CONFIGURATION" -name "*.nupkg" ! -name "*.symbols.nupkg" -type f | while read pkg; do
        echo "Pushing $(basename "$pkg")..."
        dotnet nuget push "$pkg" --api-key "$API_KEY" --source "$SOURCE" --skip-duplicate
    done
    
    # Push symbol packages
    find "bin/$CONFIGURATION" -name "*.snupkg" -type f | while read pkg; do
        echo "Pushing symbol package $(basename "$pkg")..."
        dotnet nuget push "$pkg" --api-key "$API_KEY" --source "$SOURCE" --skip-duplicate || true
    done
fi

echo ""
echo "============================================"
echo "Build completed successfully!"
echo "============================================"
echo ""

if [ "$PACK" = true ]; then
    # Extract package ID from project file
    PACKAGE_ID=$(grep -oP '<PackageId>\K[^<]+' "$PROJECT_PATH" || echo "$PROJECT_NAME")
    
    # Extract target frameworks
    TARGET_FRAMEWORKS=$(grep -oP '<TargetFrameworks>\K[^<]+' "$PROJECT_PATH" || echo "")
    
    echo "Package Information:"
    echo "  Package ID: $PACKAGE_ID"
    echo "  Version: $VERSION"
    echo "  Project: $PROJECT_NAME"
    
    if [ ! -z "$TARGET_FRAMEWORKS" ]; then
        echo "  Targets: $TARGET_FRAMEWORKS"
    fi
    
    echo ""
    echo "To install:"
    echo "  dotnet add package $PACKAGE_ID --version $VERSION"
    echo ""
fi
