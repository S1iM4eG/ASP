using System.Numerics;

namespace Blazor.Components.Pages
{
    public partial class Power
    {
        int n = 0;
        int p = 0;
        Double res = 1;
        void Multiply()
        {
            res = 1;
            if (n == 0 && p == 0) res = 0;
            else if (n != 0 && p == 0) res = 1;
            else if (n == 0 && p < 0) res = 0;
            else if (p > 0)
            {
                for (int i = 0; i < p; i++)
                {
                    res *= n;
                }
            }
            else if (p < 0)
            {
                for (int i = 0; i > p; i--)
                {
                    res *= n;
                }
                res = 1 / res;
            }
        }
    }
}
