# Boolean-algebra-calculator
A calculator for boolean algebra formulas written on C# using RPN and Shunting Yard algorithm.

![image](https://user-images.githubusercontent.com/65302409/137565334-5dc0b07a-64af-4207-aef6-8f45a2090f85.png)
![image](https://user-images.githubusercontent.com/65302409/137565371-717d9e7b-df83-4d6e-b812-da07e696bf8f.png)

## Usage
User enters amount of boolean variables, then enters a value (True or False) for each of them. There is also an input validation implemented.
Entered variables then displayed along with instruction about operations and an example of expression.
User enters a boolean formula and gets an answer (True or False).

### Available operations
* **\*** represents **Conjunction** (Logical AND)
* **\+** represents **Disjunction** (Logical OR)
* **'** represents **Negation** (Logical NOT)
* **~** represents **Equivalence** (Logical EQUALS)
* **\>** and **<** represents **Implication** (IF X THEN Y)

### Examples
Set #1 - *True*

Set #2 - *False*

Set #3 - *True*

Boolean formula:  *1+2'*(3+(1*2)')*

Answer: **True**

## Author
* Kirill Bluvband (@bloowb0und)
