# Posterr

## Project Description

Posterr is a simplified social media application similar to Twitter. It features a homepage where users can view a feed of posts, write new posts, and repost existing posts. Users can filter posts by keywords and sort them by latest or trending.

### Features

- View a feed of posts, including reposts.
- Write new posts.
- Repost existing posts (limited to original posts).
- Search posts by keywords (exact word match).
- Sort posts by latest or trending.
- Limit users to a maximum of 5 posts per day (including reposts).

## Setup Instructions

### Prerequisites

- Node.js and npm
- Docker
- .NET Core SDK

## ⚙️ Executing the Project

To execute the project, follow the steps below:

#### Execution

**Option 01: Run in Containers**

1. Run Docker Desktop.
2. Open the command prompt (cmd), navigate inside the project "\posterr" folder, and type: "docker-compose build" to build the containers (this is only necessary the first time).
3. Type "docker-compose up -d" to start the application containers.
4. To view the Web Api Swagger documentation and execute all the actions, navigate to http://localhost:8082/swagger
5. To view the front application, navigate to http://localhost:3000/

### Database Setup

1. Ensure your database is running and accessible.
2. Update the database connection string in the backend configuration file as needed.

## Critique

### Improvements

Reflecting on the project, here are some areas that I would improve if I had more time:

1. **User Authentication and Authorization**: Implementing a robust user authentication and authorization mechanism would be crucial. Currently, the app uses a hard-coded user, which is not secure or scalable. Using OAuth 2.0 or JWT (JSON Web Tokens) would enhance security.

2. **Error Handling**: While basic error handling is implemented, it can be improved to cover more edge cases and provide more informative error messages to users.

3. **Unit and Integration Testing**: Although the project might include some tests, expanding the test coverage with more unit tests and integration tests would ensure the reliability of the application. Using tools like Jest and React Testing Library for the frontend and xUnit for the backend would be beneficial.

4. **UI/UX Enhancements**: The user interface could be improved for better usability and accessibility. Adding more user feedback mechanisms, such as loading indicators and success/error messages, would enhance the user experience.

5. **Code Optimization and Refactoring**: There is always room for optimizing the codebase for better performance and readability. This includes refactoring repeated code into reusable components and improving state management.

### If this project were to grow and have many users and posts, which parts do you think would fail first?

1. **Database Performance**:

   - **Issue**: The database might become a bottleneck as the number of users and posts grows. Queries that are currently efficient may become slow when handling larger datasets.
   - **Reason**: With an increasing number of posts, the database will face challenges in managing read and write operations efficiently. The performance of complex queries, such as those used for searching and sorting posts, will degrade over time.

2. **Server Load**:

   - **Issue**: The server might struggle to handle a high number of concurrent requests, leading to slower response times or potential downtime.
   - **Reason**: As more users interact with the application simultaneously, the server’s resources (CPU, memory, etc.) may become insufficient to process all requests in a timely manner.

3. **State Management**:

   - **Issue**: The current state management approach in the frontend might not scale well with more complex state interactions and a larger number of components.
   - **Reason**: As the application grows, managing the state efficiently becomes more challenging. This can lead to performance issues and make the application harder to maintain.

4. **Search and Filtering**:
   - **Issue**: The current implementation of search and filtering may become inefficient with a large number of posts, leading to slower response times.
   - **Reason**: As the dataset grows, the time complexity of searching and filtering operations will increase, making them slower and less efficient.

### In a real-life situation, what steps would you take to scale this product? What other types of technology and infrastructure might you need to use?

1. **Database Optimization**:

   - **Indexing**: Ensure that frequently queried fields are properly indexed to speed up search operations.
   - **Sharding**: Distribute the database across multiple servers to balance the load and improve performance.
   - **Database Replication**: Use database replication to create read replicas, allowing read-heavy operations to be distributed and reducing the load on the primary database.

2. **Load Balancing**:

   - **Load Balancers**: Use load balancers (e.g., AWS Elastic Load Balancing, NGINX) to distribute incoming traffic across multiple server instances, preventing any single server from becoming overwhelmed.

3. **Horizontal Scaling**:

   - **Server Instances**: Add more server instances to handle increased traffic. This can be managed using auto-scaling features provided by cloud services such as AWS, GCP, or Azure.

4. **Microservices Architecture**:

   - **Decomposition**: Break down the monolithic application into smaller, independent microservices. Each service can be developed, deployed, and scaled independently, allowing for more efficient resource utilization and easier maintenance.

5. **Asynchronous Processing**:

   - **Message Queues**: Implement message queues (e.g., RabbitMQ, Kafka) to handle background tasks asynchronously, reducing the load on the main server and improving response times.

6. **Search Optimization**:

   - **Elasticsearch**: Use Elasticsearch to handle search functionality. Elasticsearch is designed to handle large datasets and complex queries efficiently, providing fast and scalable search capabilities.

7. **Caching**:

   - **Redis or Memcached**: Implement caching solutions to store frequently accessed data in memory, reducing the need to query the database for every request and improving response times.

8. **Monitoring and Logging**:

   - **Tools**: Use monitoring and logging tools (e.g., Prometheus, Grafana, ELK stack) to track the performance and health of the application. This helps in identifying bottlenecks and potential issues before they become critical.

9. **Content Delivery Network (CDN)**:

   - **CDN Services**: Use a CDN (e.g., Cloudflare, Amazon CloudFront) to serve static assets. CDNs cache content at edge locations, reducing the load on the main server and improving load times for users across different geographical locations.

10. **Containerization and Orchestration**:
    - **Docker and Kubernetes**: Use Docker for containerization and Kubernetes for orchestrating containerized applications. This allows for easy deployment, scaling, and management of applications across different environments.

### Conclusion

Scaling a web application to handle a growing user base involves addressing potential bottlenecks and ensuring that the infrastructure can handle increased traffic and data volume. By optimizing the database, implementing load balancing, using a microservices architecture, and leveraging modern technologies like Elasticsearch, Redis, and Kubernetes, the application can be scaled effectively to meet growing demands. Monitoring and logging are also crucial for maintaining the health and performance of the application in a real-life production environment.
