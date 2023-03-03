# OpenAPICSharp 🚀

El presente proyecto es un ejemplo de API con .NET C# utilizando Swagger (Open API)

El proyecto WebAPIUnitTests usa la db ComicTest y el proyecto WebAPI usa la db Comic. Puede cambiarla a gusto para hacer pruebas manteniedo la db original sin cambios

## Tecnologias 📌

Visual studio 2015 .NET C#

.Net Framework 4.5.2

### Pre-requisitos 📋

Tener instalada la db Comic y la db ComicTest: https://github.com/ignaciochavez/SQLServer

Tener instalado Open Xml SDK 2.5 .msi en el servidor donde se ejecutara la aplicacion: https://github.com/OfficeDev/Open-XML-SDK/releases/tag/v2.5

Tener IIS instalado

Habilitar Sitio web IIS

### Instalación 🔧

Instalar la db Comic para su uso

Configurar cadena de conexión en archivo web.config:

```
data source=NombreEquipo\NombreDB;initial catalog=Comic
```

Publicar el proyecto WebAPI desde visual studio 

Mover compilado a carpeta que leera el IIS

Habilitar directorio, sitio web IIS y SQL Server con usuario que tenga permisos del servidor

## Ejecutando las pruebas ⚙️

Una vez publicado el proyecto, abrir el sitio web IIS, redireccionar a la pagina index.html

Posteriormente Presionar en el boton Documentacion y verificar que swagger se ejecute correctamente

Ejecutar metodos del controlador check para verificar funcionalidad y autorizacion

Ejemplo de sitio inicial:
```
http://localhost:59491/index.html
```

## Construido con 🛠️

* [HtmlAgilityPack](https://html-agility-pack.net/) - Framework para el uso de HTML
* [Bootstrap](https://getbootstrap.com/) - Framework para el uso de bootstrap en el HTML
* [Swashbuckle](https://github.com/OAI/OpenAPI-Specification/blob/main/versions/2.0.md) - Framework usado para documentar y ejecutar pruebas swagger

## Autores ✒️

* **Ignacio Chávez** - *Trabajo Inicial*
* **Ignacio Chávez** - *Documentación*

© Copyright IgnacioChávez, Todos Los Derechos Reservados
