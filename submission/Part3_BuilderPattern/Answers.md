## Why is a single 20-parameter constructor for this class a problem in practice

- #### The call-site would be completely unreadable
- #### It is ease to accidentally swap two arguments of the same type
- #### If new optional property is added, we mostly would either have to implement a new 21 parameter constructor (Bad design), or modify the original one which breaks every existing call-site 
- #### `The problem not just due to constructor longness`, a class would be responsible for many fields instead of grouping related fields in a separated class

## Why is this composed version better

- #### Single responsibility for Address class and Payment class, that enhance code readability and maintenance 
- #### The parent class is not responsible for building each separated class or validate their input date, each separated class can build and validate itself
- #### separating the address class from the parent class leads to significant reduction of many redundant properties
- #### The call site become more readable 