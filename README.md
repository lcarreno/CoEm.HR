# Aplicación de Emparejamiento de Recursos Humanos

## Diseño de la base de datos

```mermaid
erDiagram
    usuario {
        int           id       PK "Clave primaria"
        nvarchar(255) email    UK "Correo electrónico único"
        varbinary(64) clave    "Contraseña en HASH(SHA2_512)"
    }
    perfil {
        int            id         PK,FK  "Clave primaria y foranea de usuario"
        nvarchar(250)  nombre            "Nombre completo o razón social"
        nvarchar(10)   telefono          "Número de teléfono"
        nvarchar(1000) direccion         "Dirección física"
        bit            es_empleador      "Si es empleador (1) o demandante (0)"
    }
    empleador {
        int           id PK,FK         "Clave primaria y foránea de perfil"
        nvarchar(125) industria        "Industria del empleador"
        int           numero_empleados "Número de empleados actual"
        nvarchar(250) ubicacion        "Ubicación principal"
    }
    demandante {
        int           id PK,FK            "Clave primaria y foránea de perfil"
        date          fecha_nacimiento    "Fecha de nacimiento"
        nvarchar(125) nivel_educacion     "Nivel de educación alcanzado"
        ntext         experiencia_laboral "Descripción de la experiencia laboral"
    }
    vacante {
        int           id           PK   "Clave primaria"
        int           empleador_id FK   "Clave foránea de EMPLEADOR"
        nvarchar(250) titulo            "Título del puesto"
        ntext         descripcion       "Descripción del puesto"
        nvarchar(125) tipo_contrato     "Tipo de contrato ofrecido"
        decimal       salario           "Salario ofrecido"
        date          fecha_publicacion "Fecha de publicación de la vacante"
    }
    aplicacion {
        int           id            PK "Clave primaria"
        int           vacante_id    FK "Clave foránea de VACANTE"
        int           demandante_id FK "Clave foránea de DEMANDANTE"
        date          fecha_aplicacion "Fecha en que se aplicó a la vacante"
        nvarchar(125) estado           "Estado de la aplicación (p.ej., pendiente, aceptada, rechazada)"
    }
    usuario ||--o| perfil : "tiene"
    perfil ||--o| empleador : "extiende"
    perfil ||--o| demandante : "extiende"
    aplicacion }o--|| demandante : "enviada por"
    vacante ||--o{ aplicacion : "recibe"
    vacante }o--|| empleador : "publicada por"
```

## Diagrama General de Arquitectura (MVC)

```mermaid
graph TD;
    Cliente[Cliente / Navegador]
    Controladores[Controladores]
    Modelos[Modelos]
    Vistas[Vistas]
    BaseDeDatos[Base de Datos SQL Server]

    Cliente -->|Solicitudes HTTP| Controladores
    Controladores -->|Devuelve| Vistas
    Controladores -->|Solicita / Recibe| Modelos
    Modelos -->|Consulta / Actualiza| BaseDeDatos
    Vistas -->|Renderiza UI| Cliente
```

## Requisitos técnicos

- .NET 8
- Docker Desktop

## Configuración de la base de datos

### Despliegue de SQL Server (Terminal)

```bash
docker run -e "ACCEPT_EULA=Y" -e 'TZ=America/Bogota' -e "MSSQL_COLLATION=Modern_Spanish_100_CI_AI_SC_UTF8" -e "MSSQL_SA_PASSWORD=Q/vX9S9zNWRVK\Zt" -p 1433:1433 --name mssqlsrv --hostname mssqlsrv -d mcr.microsoft.com/mssql/server:latest
```

### Creación de la base de datos (Cliente SQL)

```SQL
CREATE DATABASE hr_db
```

### Inicio de sesión (Cliente SQL)

```SQL
CREATE LOGIN hr_login WITH PASSWORD = 'zCkpkLYuhz95DSkc', DEFAULT_DATABASE = hr_db
```

### Usuario de la base de datos (Cliente SQL)

```SQL
USE hr_db
CREATE USER hr_user FOR LOGIN hr_login
```

### Creación del esquema (Cliente SQL)

```SQL
CREATE SCHEMA hr_schema AUTHORIZATION hr_user
ALTER USER hr_user WITH DEFAULT_SCHEMA = hr_schema
```

### Despliegue de la migración sobre la base de datos (Terminal)

```bash
dotnet ef database update
```

