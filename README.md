# Dgraph Database Example #

The [Dgraph Documentation] says:

> Dgraph is a liberally licensed, scalable, distributed, highly available and fast graph 
> database, designed from ground up to be run in production.

In this example I want to see how to work with [Dgraph] in .NET.

## Starting Dgraph ##

Initialize the Dgraph Cluster:

```
dgraph.exe zero
```

Start the Dgraph Server (with a 2048 MB LRU Cache):

```
dgraph server -l 2048
```

[Dgraph]: https://dgraph.io/
[Dgraph Documentation]: https://docs.dgraph.io/