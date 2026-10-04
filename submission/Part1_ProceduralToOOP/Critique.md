## A critique of the old code 

### 1- There is no ownership for the data or responsibility for the methods
Since this code is written in a procedural style, state is entirely detached from the behavior 
all variables live in the global scope, meaning any function can access them and modify them 

### 2- Magic numbers
`runInteractiveMenu()` depends on magic numbers to execute the proper method based on the choice and this is not a readable way
to approach it, an `enum` would be better

### 3- There is a very high probability of data inconsistency
Due to the lack of data encapsulation, this code is based on arrays to simulate data grouping for the same entity 
if any function modified any global array at a certain index and didn't modify the corresponding array at the same index,
the data would be inconsistent

### 4- Date is stored as a string
There is no validation for order date entry process

### 5- Fixed memory and data waste
The codebase depends on fixed size arrays, so we cant for example add new order if the array is full 
Additionally, for example `string customerNames[MAX_CUSTOMERS];` reserves memory in an array that might not be used  
a `list` would be better