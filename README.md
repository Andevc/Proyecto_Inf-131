# Proyecto de Aplicacion de los numeros primos 
Generacion de Claves RSA - (Para Ejecutarlo se necesita .NET Framework 4.8)

![Stared](/Data/Inicio.png)
![Generate](/Data/Generado.png)

---------------------------------------------------
### Main()

```c#
using System;

namespace RSA_keys
{
     public class rsa_key
     {
          public static void Main()
          {
               GenerateKeys genKey = new GenerateKeys();
               Console.WriteLine("Ingrese 5 digitos");

               Console.Write("p: ");
               long primoP = genKey.XPrimo(long.Parse(Console.ReadLine()));
               Console.Write("q: ");
               long primoQ = genKey.XPrimo(long.Parse(Console.ReadLine()));
               
          
               long modulo = genKey.ModKey(primoP,primoQ);
               long phi = genKey.GenPhi(primoP, primoQ);

               long e = genKey.GenCoPrimo((int)(modulo/2));

               Console.WriteLine("Module: "+modulo);
               Console.WriteLine("Phi: "+phi);
                    
               long d = genKey.InvModular(e,phi);

               while(d == 0){
                    e = genKey.GenCoPrimo((int)(modulo/2));
                    d = genKey.InvModular(e,phi);

               }

               Console.WriteLine("Public Key: "+e+" , "+modulo);
               Console.WriteLine("Private Key: "+d+" , "+modulo);
               
          }
     }
}

```

## Clase GenerateKeys
```csharp
using System;
namespace RSA_keys
{
   public class GenerateKeys
   {  
      public static bool EsPrimo(long num)
      {
         if (num <= 1) return false;
         if (num == 2) return true;
         if (num % 2 == 0) return false;
         long boundary = (int)Math.Floor(Math.Sqrt(num));
         for (int i = 3; i <= boundary; i += 2)
         {
            if (num % i == 0) return false;

         }
         return true;
      }

      public long XPrimo(long n)
      {
         int contP = 0;
         int primo = 2;
         long x = 0;
         while (contP < n)
         {
            if (EsPrimo(primo))
            { x = primo; contP++; }
            primo++;
         }
         return x;
      } 
      
      public long ModKey(long p, long q){ return (p*q); }
      public long GenPhi(long p, long q){ return ((p-1)*(q-1)); }

      // Calculamos si e es coprimo con el MCD
      public long GenCoPrimo(int x){
         Random rnd = new Random();
         if(x < 0){ x *= -1;}

         while(true){
            long y = rnd.Next(2,x);
            if(EsCoprimo(x,y)){ return y; }
         }         
      }

      public bool EsCoprimo(long x, long y) { return MCD(x,y) == 1; }

      public static long MCD(long x, long y)
      {
         while (y != 0)
         {
            long aux = y;
            y = x % y;
            x = aux;
         }
         return x;
      }

      // Calcula el inverso modular

      public long InvModular(long e, long phi){
         // Calcula el MCD y los coeficientes de Bézout usando el algoritmo extendido de Euclides
         EuclidesExtendido(e, phi, out long mcd, out long x, out _);

         // Si el MCD no es 1, entonces retornamos 0 para que se vuelva a generar un nuevo CoPrimo
         if (mcd != 1) { return 0; }

         // Calcula el inverso modular usando el coeficiente x

         long inverso = x % phi;
         if (inverso < 0) { inverso += phi; } // Si el inverso es negativo, lo ajustamos sumando n
         return inverso;

      }
      static void EuclidesExtendido(long a, long b, out long mcd, out long x, out long y)
      {
         long x0 = 1, y0 = 0, x1 = 0, y1 = 1;

         while (b != 0)
         {
            long q = a / b;
            long temp = b;
            b = a % b;
            a = temp;

            long xx = x0 - q * x1;
            long yy = y0 - q * y1;

            x0 = x1;
            y0 = y1;
            x1 = xx;
            y1 = yy;
         }

         mcd = a;
         x = x0;
         y = y0;
      }
   }
}


```
