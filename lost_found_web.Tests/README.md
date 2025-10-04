# Lost & Found Web - Test Suite

This test suite provides comprehensive testing for the Lost & Found web application.

## Test Structure

### Services Tests
- **LostFoundServiceTests.cs** - Unit tests for the core business logic service
  - Tests for Lost Items CRUD operations
  - Tests for Found Items CRUD operations
  - Tests for search functionality
  - Tests for edge cases and error conditions

### Model Tests
- **LostItemTests.cs** - Validation tests for LostItem model
- **FoundItemTests.cs** - Validation tests for FoundItem model
  - Tests for required field validation
  - Tests for string length validation
  - Tests for default values

### Integration Tests
- **WebApplicationFactory.cs** - Test web application factory
- **LostItemsIntegrationTests.cs** - End-to-end tests for Lost Items pages
- **FoundItemsIntegrationTests.cs** - End-to-end tests for Found Items pages
- **SearchIntegrationTests.cs** - End-to-end tests for Search functionality
- **HomePageIntegrationTests.cs** - Tests for main application pages

### Test Utilities
- **TestDataBuilder.cs** - Helper methods for creating test data
- **TestExtensions.cs** - Extension methods for model validation testing

## Test Categories

### Unit Tests
- Service layer business logic
- Model validation
- Utility functions

### Integration Tests
- HTTP endpoint testing
- Page rendering
- Form submission
- Navigation flows

## Running Tests

```bash
# Run all tests
dotnet test

# Run tests with coverage
dotnet test --collect:"XPlat Code Coverage"

# Run specific test category
dotnet test --filter "Category=Unit"
dotnet test --filter "Category=Integration"
```

## Test Dependencies

- **xUnit** - Testing framework
- **FluentAssertions** - Fluent assertion library
- **Microsoft.AspNetCore.Mvc.Testing** - Integration testing for ASP.NET Core
- **Moq** - Mocking framework (for future use)

## Test Data

The test suite includes:
- Valid test data builders
- Invalid test data scenarios
- Edge case data (long strings, null values)
- Sample data for integration testing

## Coverage

The test suite aims to cover:
- ✅ Service layer (100%)
- ✅ Model validation (100%)
- ✅ HTTP endpoints (100%)
- ✅ Page rendering (100%)
- ✅ Form validation (100%)
- ✅ Error handling (100%)

## Best Practices

1. **Arrange-Act-Assert** pattern used throughout
2. **FluentAssertions** for readable test assertions
3. **Test data builders** for consistent test data creation
4. **Integration tests** use real HTTP client
5. **Unit tests** focus on business logic isolation
6. **Comprehensive edge case testing**
