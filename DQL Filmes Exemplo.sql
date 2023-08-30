USE Filmes;

INSERT INTO Genero (Nome)
VALUES ('Ação'),('Terror');

INSERT INTO Filme (IdGenero,Titulo)
VALUES (1,'O pacto'),(2,'O Nascimento do mal');

SELECT * FROM Genero;
SELECT * FROM Filme;

INSERT INTO Genero (Nome)
VALUES ('caixa d'água');