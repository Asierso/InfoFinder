# InfoFinder
Busca información en Google automáticamente. Solo introduce el tema a buscar, el número de listas de páginas que quieras que consulte y los elementos que quieras filtrar el programa generará una carpeta con todos los resultados (imagenes, Pdfs y texto)

## Requisitos

* Google Chrome Versión 98
* .Net Framework v4.7.2

## Filtros

| Código a usar | Elementos que afecta |
| ------------- | ------------- |
| `p`  | Todo el texto contenido en párrafos  |
| `h1 - h6`  | Todo el texto contenido en títulos del nivel indicado |
| `img` | Cualquier recurso de imágen encontrado |
| otros "HTML" | Todo el contenido de dentro del tipo indicado |
| `*` | Guarda todos los párrafos, títulos de primer a cuarto nivel e imágenes |

## Bibliografías

InfoFinder almacena todas las páginas de las cuales se han buscado/revisado información en un archivo, donde referencia la URL de la web buscada con la carpeta con el contenido extraído de esta.

## Búsqueda masiva

InfoFinder puede buscar contenidos ordenados dentro de un archivo .csv delimitado por `;`. La estructura de etos archivos debe de ser la siguiente

| A | B | C | D |
| ------------- | ------------- | ------------- | ------------- | 
| Información a buscar (1)  | Información a buscar (2)  | Información a buscar (3) | ... |
| Número de listas (1) | Número de listas (2) | Número de listas (3) | ... |
| Filtros (1) | Filtros (2) | Filtros (3) | ... |
