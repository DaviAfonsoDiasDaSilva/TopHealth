-- 1. Criação de tabelas com chaves estrangeiras

CREATE TABLE Usuario (
  id INTEGER PRIMARY KEY AUTOINCREMENT,
  Nome TEXT NOT NULL,
  Sobrenome TEXT NOT NULL,
  email TEXT NOT NULL,
  senha TEXT NOT NULL
);

CREATE TABLE Alimentacao (
  id INTEGER PRIMARY KEY AUTOINCREMENT,
  Descricao TEXT NOT NULL,
  ValorEnergetico INTEGER NOT NULL
);

CREATE TABLE AtividadeFisica (
  id INTEGER PRIMARY KEY AUTOINCREMENT,
  TipoAtividade TEXT NOT NULL,
  DuracaoMinutos INTEGER NOT NULL
);

CREATE TABLE Humor (
  id INTEGER PRIMARY KEY AUTOINCREMENT,
  Descricao TEXT NOT NULL
);

CREATE TABLE QualidadeSono (
  id INTEGER PRIMARY KEY AUTOINCREMENT,
  Descricao TEXT NOT NULL
);

CREATE TABLE Configuracao (
  UserId INTEGER PRIMARY KEY,
  CaminhoBanco TEXT NOT NULL,
  Tema TEXT NOT NULL,
  FlagImportacao INTEGER NOT NULL,
  FOREIGN KEY (UserId) REFERENCES Usuario(id)
);

CREATE TABLE RegistroDiario (
  id INTEGER PRIMARY KEY AUTOINCREMENT,
  UserId INTEGER NOT NULL,
  Data TEXT NOT NULL, 
  HumorId INTEGER NOT NULL,
  SonoId INTEGER NOT NULL,
  AlimentacaoId INTEGER NOT NULL,
  AtividadeFisicaId INTEGER NOT NULL,
  FOREIGN KEY (UserId)           REFERENCES Usuario(id),
  FOREIGN KEY (HumorId)          REFERENCES Humor(id),
  FOREIGN KEY (SonoId)           REFERENCES QualidadeSono(id),
  FOREIGN KEY (AlimentacaoId)    REFERENCES Alimentacao(id),
  FOREIGN KEY (AtividadeFisicaId)REFERENCES AtividadeFisica(id)
);

-- 2. Inserts na ordem certa

INSERT INTO Usuario (Nome, Sobrenome, email, senha) VALUES
('Exemplício', 'Exemplificador de Exemplos', 'genericsample@gmail.com', 'e8d95a51f3af4a3b134bf6bb680a213a');

INSERT INTO Alimentacao (Descricao, ValorEnergetico) VALUES
('Filet mignon aux demi-glace', 600);

INSERT INTO AtividadeFisica (TipoAtividade, DuracaoMinutos) VALUES
('corrida', 60);

INSERT INTO Humor (Descricao) VALUES
('feliz');

INSERT INTO QualidadeSono (Descricao) VALUES
('boa');

INSERT INTO Configuracao (UserId, CaminhoBanco, Tema, FlagImportacao) VALUES
(1, 'arquivo onde ficará o banco de dados', 'escuro', 1);

-- Agora que todos os FK = 1 existem, use ids válidos (todos 1)
INSERT INTO RegistroDiario
  (UserId, Data, HumorId, SonoId, AlimentacaoId, AtividadeFisicaId)
VALUES
  (1, '2025-04-30', 1, 1, 1, 1);
