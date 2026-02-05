# DotnetDDD

A learning sandbox for building a .NET backend using Domain-Driven Design (DDD) and Clean Architecture — moving beyond pattern-by-repetition toward deliberate, well-structured design.

---

## Problem Statement

My current API system is written in .NET. I'm fairly new to C#, and every feature so far has been built on **pattern recognition** — "there must be a controller file, a service file, a repository file, an entity folder, a contract folder, etc." I follow the structure without fully understanding _why_ each piece exists or how they should interact.

## What I Have Now

### Architecture Style

- N-layered: Controller → Service → Repository
- Predefined contract interfaces for services
- Inversion of Control and Dependency Injection at the constructor level

### Tools & Stack

- **ORM**: Dapper instead of EF Core, based on the assumption that EF has poor performance — though issues like N+1 queries and improper use of `.ToList()` in EF can't be ruled out as the real concern
- **Auth**: JWT middleware using the Identity package, but without leveraging features like RBAC
- **Database**: SQL Server, some PostgreSQL
- **Missing**:
  - No data validation
  - No structured error handling
  - No unit tests
  - No logging
  - No caching

## What I Want to Learn

- Understand _why_ code is organized a certain way, not just _how_
- Apply DDD tactical patterns (Entities, Value Objects, Aggregates, Domain Events)
- Use Clean Architecture to enforce proper dependency direction
- Adopt standard, well-supported tools in the .NET ecosystem
- Write testable code with proper validation and error handling
- Build something that can run in Docker

---

## Example Domain: Online Bookstore

An online bookstore is used as the learning domain. It's simple enough to start quickly, but rich enough to demonstrate real DDD patterns — and it naturally evolves into event-driven and distributed patterns in Phase 2.

### Bounded Contexts

| Context       | Responsibility                      | Key Concepts                                   |
| ------------- | ----------------------------------- | ---------------------------------------------- |
| **Catalog**   | Managing books, authors, categories | Book, Author, Category, ISBN (Value Object)    |
| **Ordering**  | Placing and managing orders         | Order (Aggregate Root), OrderLine, OrderStatus |
| **Customers** | Customer profiles and addresses     | Customer, Address (Value Object)               |
| **Inventory** | Stock levels, reservations          | StockItem, Reservation                         |

### Why This Domain Works

- **Aggregates**: An `Order` naturally groups `OrderLines` — you can't modify a line without going through the order
- **Value Objects**: `ISBN`, `Money`, `Address` are classic examples of immutable types with no identity
- **Domain Events**: `OrderPlaced` → triggers `StockReserved` → naturally leads into messaging in Phase 2
- **Bounded Contexts**: Catalog and Ordering have different views of a "Book" — good for learning context boundaries

---

## Phase 1: Clean Architecture + DDD

### Architecture

Clean Architecture (inspired by Hexagonal / Ports & Adapters):

```
Domain          → Entities, Value Objects, Aggregates, Domain Events, Repository interfaces
Application     → Use Cases, DTOs, Validation, Application Services (orchestration)
Infrastructure  → EF Core, Database, External Services, Repository implementations
API             → Controllers, Middleware, DI configuration
```

Dependencies point inward: `API → Application → Domain` and `Infrastructure → Application → Domain`. Domain has **zero** external dependencies.

### Target Stack

| Concern         | Tool / Library               | Why                                                             |
| --------------- | ---------------------------- | --------------------------------------------------------------- |
| ORM             | EF Core                      | Industry standard, great migration support, LINQ                |
| Validation      | FluentValidation             | Declarative rules, clean separation from domain                 |
| Mediator / CQRS | MediatR                      | Decouples handlers from controllers, enables pipeline behaviors |
| Logging         | Serilog                      | Structured logging, multiple sinks                              |
| Testing         | xUnit + Moq (or NSubstitute) | Most popular .NET test stack                                    |
| Containers      | Docker + Docker Compose      | Consistent environments, easy DB setup                          |
| Database        | PostgreSQL                   | Open source, runs well in Docker, SQL-based                     |

### Phase 1 Success Criteria

- [x] Domain layer with at least one proper Aggregate (Order), Value Objects (Money, ISBN), and a Domain Event
- [x] Application layer with use cases implemented via MediatR handlers, FluentValidation, and CQRS separation
- [x] Infrastructure layer with EF Core persistence
- [ ] API layer with controllers that delegate to MediatR
- [ ] FluentValidation for input validation
- [ ] Serilog for structured logging
- [ ] Unit tests for domain logic and application use cases
- [ ] Integration tests for the API layer
- [ ] Docker Compose setup (API + PostgreSQL)
- [ ] No business logic in controllers or infrastructure

---

## Phase 2: Modern Distributed Patterns

Building on Phase 1's foundation, Phase 2 introduces patterns needed for systems at scale.

### 2.1 CQRS (Command Query Responsibility Segregation)

Separate the write model (commands) from the read model (queries) — the bookstore's catalog reads are far more frequent than order writes, making it a natural fit.

**Goals:**

- [ ] Split MediatR handlers into explicit Command and Query handlers
- [ ] Create dedicated read models / projections optimized for queries (e.g., `BookListView`, `OrderSummaryView`)
- [ ] Use Dapper or raw SQL for read-side queries (performance-optimized)
- [ ] Keep EF Core for the write side (domain integrity)

### 2.2 Messaging & Event-Driven Architecture

Replace in-process domain events with asynchronous messaging between bounded contexts.

**Goals:**

- [ ] Set up RabbitMQ (or MassTransit as an abstraction) in Docker
- [ ] Publish domain events as integration events (e.g., `OrderPlaced` → `OrderPlacedIntegrationEvent`)
- [ ] Inventory service subscribes to `OrderPlaced` and reserves stock asynchronously
- [ ] Implement the Outbox Pattern to guarantee event delivery (no lost messages)
- [ ] Handle idempotency — consumers can safely process the same message twice

### 2.3 Microservices Decomposition

Break the monolith into independently deployable services along bounded context boundaries.

**Goals:**

- [ ] Extract Catalog and Ordering into separate services with their own databases
- [ ] API Gateway (YARP or Ocelot) to route requests
- [ ] Each service owns its data — no shared databases
- [ ] Inter-service communication via messaging (not direct HTTP calls for commands)
- [ ] Distributed tracing with OpenTelemetry
- [ ] Health checks and resilience (Polly for retries, circuit breakers)

### Phase 2 Success Criteria

- [ ] Commands and queries are clearly separated with different models
- [ ] At least two services communicate via a message broker
- [ ] Outbox pattern ensures reliable event publishing
- [ ] Services run independently in Docker Compose
- [ ] Distributed tracing shows request flow across services

---

## References

- [Microsoft: Domain-Driven Design (eShopOnWeb reference app)](https://github.com/dotnet-architecture/eShopOnWeb)
- [Microsoft: .NET Microservices Architecture e-book](https://learn.microsoft.com/en-us/dotnet/architecture/microservices/)
- [Vaughn Vernon: Implementing Domain-Driven Design (book)](https://www.oreilly.com/library/view/implementing-domain-driven-design/9780133039900/)

---

## Project Structure

```
DotnetDDD/
├── DotnetDDD.sln
├── docker-compose.yml
├── README.MD
└── src/
    ├── Domain/                          # Entities, Value Objects, Aggregates, Domain Events
    │   ├── Domain.csproj
    │   ├── Common/                      # Entity, AggregateRoot, ValueObject, IDomainEvent, DomainException
    │   ├── Catalog/                     # Book, Author, Category, Isbn (VO), IBookRepository
    │   ├── Ordering/                    # Order (AR), OrderLine, OrderStatus, Money (VO), IOrderRepository
    │   ├── Customers/                   # Customer, Address (VO), ICustomerRepository
    │   └── Inventory/                   # StockItem, IStockItemRepository
    │
    └── Application/                     # Use Cases, DTOs, Validation, MediatR handlers
        ├── Application.csproj           # References Domain; MediatR, FluentValidation
        ├── DependencyInjection.cs       # Service registration
        ├── Common/
        │   ├── Messaging/               # ICommand, IQuery, ICommandHandler, IQueryHandler
        │   └── Behaviors/               # ValidationBehavior (MediatR pipeline)
        ├── Catalog/
        │   ├── BookResponse.cs
        │   ├── CreateBook/              # Command, Handler, Validator
        │   └── GetBook/                 # Query, Handler
        ├── Ordering/
        │   ├── OrderResponse.cs
        │   ├── CreateOrder/             # Command, Handler, Validator
        │   ├── AddOrderLine/            # Command, Handler, Validator
        │   ├── CancelOrder/             # Command, Handler, Validator
        │   └── GetOrder/                # Query, Handler
        ├── Customers/
        │   ├── CustomerResponse.cs
        │   ├── CreateCustomer/          # Command, Handler, Validator
        │   └── GetCustomer/             # Query, Handler, Validator
        └── Inventory/
            ├── InventoryResponse.cs
            ├── AddStock/                # Command, Handler, Validator
            ├── GetStock/                # Query, Handler
            └── ReserveStock/            # Command, Handler, Validator
```

> Infrastructure and API projects will be added next.
