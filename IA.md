# Uso de Inteligencia Artificial - Taller 2



### 1. Consulta:
"¿Cuál es la diferencia conceptual entre el modelo de comando y el modelo de consulta en CQRS? Explica con un ejemplo de una base de datos relacional por qué no deberíamos usar la misma entidad de base de datos para lectura y escritura."

### 2. Consulta:
"Tengo una entidad Link con Id, Url original, ShortUrl, Clicks, UserId y navegación a User. Para un modelo de consulta optimizado, ¿qué campos debería incluir en el modelo de lectura para agilizar las vistas?"

### 3. Consulta: 
"En CQRS, ¿cómo se configuran dos repositorios separados usando EF Core? ¿Es recomendable usar el mismo AppDbContext pero con diferentes configuraciones de tracking, o usar contextos distintos?"

### 4. Consulta:
"¿Cómo estructurar un repositorio de solo lectura en EF Core usando AsNoTracking y omitiendo métodos como SaveChanges para garantizar que sea de solo lectura?"



### 5. Consulta:
"Cómo definir una entidad de lectura desnormalizada LinkReadModel en Entity Framework Core, optimizada exclusivamente para consultas sin sobrecargar la entidad de dominio de escritura?"

### 6. Consulta:
"Cómo implementar la interfaz ILinkReadRepository y la clase LinkReadRepository para consultar únicamente la tabla link_read_models aplicando .AsNoTracking() en cada método de consulta?"

### 7. Consulta:
"Cómo estructurar las Queries (GetLinkByShortUrlQuery, GetAllLinksQuery, GetLinksByUserIdQuery) y sus respectivos Handlers para retornar DTOs de lectura LinkResponse?"

### 8. Consulta:
"Cómo refactorizar los controladores/vistas en Index.cshtml.cs y los endpoints en UrlRedirectEndpoint.cs para inyectar los Query Handlers en lugar de depender del servicio antiguo ILinkService?"

