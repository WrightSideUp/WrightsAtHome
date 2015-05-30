
grammar Trigger;

endTrigger       : afterExp | trigger    ;
trigger          : atExp | whenExp   ;

afterExp         : AFTER duration       ;
duration         : INT timePeriod ;
timePeriod       : HOURS | MINUTES ;

atExp            : AT TIMECONST ;

whenExp          : WHEN expr;

expr             :    MINUS expr									#Negation
                    | expr (MUL | DIV) expr							#MulDiv
					| expr (PLUS | MINUS) expr						#AddSub
					| expr (LESSTHAN | LESSEQUAL) expr				#LessThan
					| expr (EQUAL | NOTEQUAL) expr					#Equality
					| expr (GREATERTHAN | GREATEREQUAL) expr		#GreaterThan
					| expr (AND | OR) expr							#Logical
					| NOT expr										#Not
					| ID											#Identifier
					| INT											#Atom
					| DECIMAL										#Atom
					| STRING										#Atom
					| '(' expr ')'									#Paren;
					
AFTER : 'after';
WHEN  : 'when' ;
AT    : 'at' ;

MUL : '*';
DIV : '/';
PLUS : '+';
MINUS: '-';
LESSTHAN: '<';
LESSEQUAL : '<=';
EQUAL : '=';
NOTEQUAL : '<>';
GREATERTHAN : '>';
GREATEREQUAL : '>=';
AND : 'and';
OR : 'or';
NOT : 'not';

MINUTES: 'minute'('s')? ;
HOURS : 'hour'('s')? ;

TIMECONST : HOURPART MINSSECS | HOURPART MINSSECS MINSSECS | HOURPART MINSSECS? MINSSECS? AMPM;	
fragment HOURPART : '1'[0-2] | [1-9];
fragment MINSSECS : ':' [0-5] [0-9];
fragment AMPM : 'am' | 'pm';

STRING : '"'  (ESCAPED_QUOTE | ~('\n' | '\r') )*? '"';
fragment ESCAPED_QUOTE : '\\"';

DECIMAL : INT '.' INT;

INT :    DIGIT+;
fragment DIGIT : [0-9];

ID :     LETTER (LETTER | DIGIT)*;
fragment LETTER :  [a-zA-Z];

WS : [ \t\r\n]+ -> skip;
ErrorChar : . ;