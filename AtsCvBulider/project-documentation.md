# ATS CV Builder Project Documentation

## Project Overview

ATS CV Builder is a web application designed to help users create, manage, and optimize their CVs/resumes for Applicant Tracking Systems (ATS). The application provides a secure platform where users can register, log in, and create multiple CVs with different sections such as education, experience, skills, etc.

## Technical Stack

- **Backend**: ASP.NET Core Web API (.NET 6+)
- **Database**: SQL Server with Entity Framework Core
- **Authentication**: JWT (JSON Web Tokens)
- **API Documentation**: Swagger/OpenAPI

## Architecture

The project follows a standard layered architecture:

1. **Controllers Layer**: Handles HTTP requests and responses
2. **Services Layer**: Contains business logic
3. **Data Layer**: Manages data access through Entity Framework Core
4. **Models**: Defines the data structures
5. **DTOs (Data Transfer Objects)**: Used for data transfer between client and server

## Core Features

### User Management

- User registration with email, password, full name, date of birth, and description
- User authentication using JWT tokens
- User profile management (update profile information, change password)

### CV Management

- Create multiple CVs with title and summary
- View all user CVs
- View a specific CV by ID
- Update existing CVs
- Delete CVs

### CV Section Management

- Add different sections to a CV (e.g., education, experience, skills)
- Update CV sections
- Delete CV sections

## Security Features

- JWT-based authentication
- Password hashing with ASP.NET Core Identity
- Authorization policies to ensure users can only access their own data
- Email verification (marked in the model but implementation may be pending)

## API Endpoints

The API is organized into three main controllers:

1. **Auth Controller**: Handles user registration and authentication
2. **User Controller**: Manages user profile operations
3. **CV Controller**: Handles CV and CV section operations

For detailed API documentation, refer to the `api-endpoints.json` file which contains comprehensive information about all endpoints, request/response structures, and authentication requirements.

## Data Models

### ApplicationUser

Extends the ASP.NET Core Identity User with additional properties:
- Full Name
- Date of Birth
- Description
- Email Verification Status
- Collection of CVs

### CV

Represents a user's CV with:
- Title
- Summary
- Collection of CV Sections
- Reference to the user who owns it

### CvSection

Represents a section within a CV:
- Section Type (e.g., "Education", "Experience")
- Content
- Reference to the parent CV

## Getting Started for Frontend Developers

1. **API Base URL**: The API is configured to accept requests from specific origins (see CORS settings in Program.cs)
2. **Authentication**: 
   - Register a user using the `/api/auth/register` endpoint
   - Log in using the `/api/auth/login` endpoint to receive a JWT token
   - Include the JWT token in the Authorization header for authenticated requests: `Authorization: Bearer {token}`
3. **API Documentation**: When running the application in development mode, Swagger UI is available to explore the API

## Future Enhancements

Based on the codebase review, potential future enhancements might include:
- Implementation of email verification
- Password reset functionality
- More sophisticated CV templates
- ATS optimization suggestions
- Export to different formats (PDF, Word, etc.)
- CV sharing capabilities

## Notes for Frontend Integration

- All authenticated endpoints require a valid JWT token in the Authorization header
- The API returns appropriate HTTP status codes and error messages
- For CV sections, the content field can store structured data (e.g., JSON) to represent complex section content
- The API is designed to handle CORS for specified origins

For detailed API documentation including all endpoints, request/response formats, and authentication requirements, please refer to the `api-endpoints.json` file. 