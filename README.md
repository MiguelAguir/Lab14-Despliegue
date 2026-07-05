# Lab14-Despliegue

API de generación de reportes empresariales en Excel construida con ASP.NET Core Minimal API para despliegue en Render con Docker.

## Descripción

API minimalista que expone endpoints para generar reportes en formato Excel (.xlsx) a partir de datos almacenados en PostgreSQL. Utiliza ClosedXML para la creación de archivos Excel y está preparada para ser desplegada en la nube mediante contenedores Docker en Render.

## Tecnologías

- **.NET 9** / ASP.NET Core Minimal API
- **Entity Framework Core 9** con PostgreSQL (Npgsql)
- **ClosedXML** para generación de archivos Excel
- **Swagger / OpenAPI** para documentación
- **Docker** para contenerización
- **Render** como plataforma de despliegue

## Estructura del proyecto

```
Lab14-Despliegue/
  Models/        -- Entidades: Client, Order, OrderDetail, Product
  Data/          -- AppDbContext (EF Core)
  Services/      -- IReportService, ReportService (generación de reportes)
  Program.cs     -- Configuración y definición de endpoints
  Dockerfile     -- Imagen multi-etapa para despliegue
```

## Instalación

1. Clonar el repositorio:
   ```bash
   git clone https://github.com/MiguelAguir/Lab14-Despliegue.git
   cd Lab14-Despliegue
   ```

2. Configurar la cadena de conexión en `appsettings.json`:
   ```json
   {
     "Host=localhost;Port=5432;Database=LINQExample;Username=postgres;Password=..."
   }
   ```

3. Ejecutar:
   ```bash
   dotnet run
   ```

## Endpoints

| Método | Ruta                              | Descripción                                    |
|--------|-----------------------------------|------------------------------------------------|
| GET    | /reporte/pedidos-cliente          | Descargar reporte de pedidos agrupados por cliente (Excel) |
| GET    | /reporte/productos-vendidos       | Descargar reporte de productos más vendidos (Excel)       |
| GET    | /weatherforecast                  | Endpoint de prueba (por defecto)               |

## Docker

Construir y ejecutar localmente:

```bash
docker build -t reportes-api .
docker run -p 8080:8080 reportes-api
```

La imagen usa una construcción multi-etapa con SDK 9.0 para compilar y runtime 9.0 para ejecución. Expone el puerto configurado mediante la variable de entorno `$PORT` para compatibilidad con Render.

## Despliegue en Render

1. Conectar el repositorio a Render
2. Usar el Dockerfile incluido
3. Configurar la variable de entorno `PORT` (asignada automáticamente por Render)
4. Configurar `ConnectionStrings__DefaultConnection` con la cadena de PostgreSQL

## Estado del proyecto

Completado. Listo para despliegue y uso en producción.
