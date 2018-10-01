%{
// Ёти объ€влени€ добавл€ютс€ в класс GPPGParser, представл€ющий собой парсер, генерируемый системой gppg
    public Parser(AbstractScanner<int, LexLocation> scanner) : base(scanner) { }
%}

%output = SimpleYacc.cs

%namespace SimpleParser

%token BEGIN END CYCLE INUM RNUM ID ASSIGN SEMICOLON WHILE DO REPEAT UNTIL FOR TO WRITE OPENBRACKET CLOSEBRACKET IF THEN ELSE

%%

progr   : block
		;

stlist	: statement 
		| stlist SEMICOLON statement 
		;

statement: assign
		| block  
		| cycle  
		| while
		| repeat
		| for
		| write
		| if
		;

ident 	: ID 
		;
	
assign 	: ident ASSIGN expr 
		;

expr	: ident  
		| INUM 
		;

block	: BEGIN stlist END 
		;

cycle	: CYCLE expr statement 
		;

while	: WHILE expr DO statement
		;

repeat	: REPEAT stlist UNTIL expr
		;

for		: FOR ID ASSIGN expr TO expr DO statement
		;

write	: WRITE OPENBRACKET expr CLOSEBRACKET
		;

if		: IF expr THEN statement
		| IF expr THEN statement ELSE statement
		;

%%
