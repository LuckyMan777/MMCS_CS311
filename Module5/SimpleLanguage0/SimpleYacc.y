%{
// ��� ���������� ����������� � ����� GPPGParser, �������������� ����� ������, ������������ �������� gppg
    public Parser(AbstractScanner<int, LexLocation> scanner) : base(scanner) { }
%}

%output = SimpleYacc.cs

%namespace SimpleParser

%token BEGIN END CYCLE INUM RNUM ID ASSIGN SEMICOLON WHILE DO REPEAT UNTIL FOR TO WRITE OPENBRACKET CLOSEBRACKET IF THEN ELSE VAR COMMA PLUS MINUS MULTIPLE DIVIDE

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
		| var
		;

ident 	: ID 
		;
	
assign 	: ident ASSIGN expr 
		;

//expr	: ident  
//		| INUM 
//		;

expr	: T
		| expr PLUS T
		| expr MINUS T
		;

T		: F
		| T MULTIPLE F
		| T DIVIDE F
		;

F		: ident
		| INUM 
		| OPENBRACKET expr CLOSEBRACKET
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

//partvar	: ID
//		| partvar COMMA ID

var		: VAR partvar
		;

partvar	: ID
		| partvar COMMA ID
		;

%%
