# ImpedexTracker

A repair job tracking API built for Impedex, a family electronics repair business.

## Built With
- ASP.NET Core 10
- Entity Framework Core 9
- SQL Server
- Swagger UI

## Features
- Create, read, update, and delete repair jobs
- Track job status (Received, In Progress, Completed)
- Track received and completed dates
- RESTful API with full Swagger documentation

## Endpoints
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | /api/jobs | Get all jobs |
| GET | /api/jobs/{id} | Get job by ID |
| POST | /api/jobs | Create new job |
| PUT | /api/jobs/{id} | Update job |
| DELETE | /api/jobs/{id} | Delete job |

## Purpose
This project was built to enhance ASP.NET Core and Entity Framework Core knowledge while solving a real business problem.
