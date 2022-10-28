# OpenAPICSharp 🚀

El presente proyecto es un ejemplo de API con C# utilizando Swagger

## Tecnologias 📌

Visual studio 2015 .NET C#

.Net Framework 4.5.2

### Pre-requisitos 📋

Tener codigo fuente de solucion

Tener instalado Open Xml SDK 2.5 .msi (https://github.com/OfficeDev/Open-XML-SDK/releases/tag/v2.5)

Tener IIS instalado

Habilitar Sitio web IIS

### Instalación 🔧

Copiar archivo WebAPI.XML ubicado en la carpeta bin del proyecto WebAPI de la solución visual studio 2015, y pegar el archivo en la carpeta bin del compilado a implementar (si no existe el archivo, se genera al recompilar solucion desde visual studio)

Habilitar directorio y sitio web IIS

## Ejecutando las pruebas ⚙️

Ejecutar pruebas unitarias desde visual studio para verificar correcto funcionamiento

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
