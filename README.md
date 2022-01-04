# Packages(Share local data)


---------------------------------
  ***development phase suspended*** ||| ***development phase suspended*** ||| ***development phase suspended*** ||| ***development phase suspended*** 

---------------------------------


[![NuGet version (Groophy.Packages)](https://img.shields.io/nuget/v/Groophy.Packages.svg?style=flat-square)](https://www.nuget.org/packages/Groophy.LangPackage/)

[Source Code](https://github.com/Groophy-Inc/Groophy.Packages/blob/main/Groophy.Packages/Web.cs)

## Usage

### Server Side
```c#
Package p = new Package();

//http://localhost:5000/
Web w = new Web();

w.run(new Configure
{
    Port = 5000
});
w.onData += w_onData;
```

```c#
static void w_onData(Web sender, User e)
{
    if (e.Query["Groophy"] == "Lifefor")
    {
        e.Response("Welcome");
    }
    else
    {
        e.Response("Access Denied");
    }

    e.Kill();
}
```

### Client Side
```c#
Web c = new Web();

c.Client(new ConfigureClient
{
    Port=5000,
    param = new paramst[] { new paramst()
    {
        var = "Groophy", val="Lifefor"
    }},
});
c.onClient += c_onClient;
```

```c#
static void c_onClient(Web sender, System.Net.Http.HttpResponseMessage Context, string response)
{
    Console.WriteLine("Reponse: " + response);
}
```

## Tests
 - Connection with random query(http://localhost:5000/?df=fgv)

![rndm](https://user-images.githubusercontent.com/77299279/147703875-0c1b1051-e322-4971-98d9-8539aefe90b4.PNG)

 - Connection with set query(http://localhost:5000/?Groophy=Lifefor)
 
![true](https://user-images.githubusercontent.com/77299279/147703926-503a0675-b170-470e-b786-5cee89904d97.PNG)

~Groophy Lifefor
