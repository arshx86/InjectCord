# InjectCord
Static discord code injector library. Easily inject **custom** javascript code into your discord client.

## How it works
**Discord voice module** is only file that can be exploited. 
By writing a custom code into **modules\\discordvoice\\index.js** file you can run **custom** code or exploit on discord startup.
Writing other module's **indexes** won't work. They're brokes the client.
**InjectCord** can find the this file auto (path's and versions are different on each client) and exploit this **index**.

## Injecting 
Do with one method, put the code into method and exploit it.
```csharp
InjectCord.Inject("code");
```
I.e, let's inject some garbage code to test if it's working.
```csharp
InjectCord.Inject("console.clear(); console.log('im in!'); ");
```
![image](https://user-images.githubusercontent.com/85416153/172498646-6e757c8d-9d66-4187-b218-c77b0801cc63.png)

> This code will be executed on **discord launch**. Code cannot applied in **runtime** so client needs to restart. You can pass **ApplyNow: true** param to restart it to take effects.

> For bigger codes, i suggest you download code from **link** or reading via **file**.

> Wrong code injection (which isn't **native**/**pure** javascript code) may broke the client. 

## UnInjecting
To revert back your client state to default, you can use `InjectCord.Revert();` method. That will grab original source of exploited file and replace with your existing corrupt file. 

## Sample App
You can use sample app to try injection. Download in releases. `(As it's so buggy, i don't recommend to use it.)`
![image](https://user-images.githubusercontent.com/85416153/172498800-750d9b7e-c0b0-40ac-a652-d5f12034a6f8.png)

## Compatibility
> Only works on **windows**/**stable** build. Feel free to add **canary, ptb** support **PR**.
