SELECT * FROM EVENTOS E
INNER JOIN ENDERECOS EN ON E.Id = EN.EventoId
INNER JOIN CATEGORIAS C ON E.CategoriaId = C.Id
INNER JOIN ORGANIZADORES O ON O.Id = E.OrganizadorId