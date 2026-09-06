using System.Numerics;

namespace Blazor.Components.Pages
{
    public partial class Fibonacci
    {
        int count;
        long[] res;

        void Сalculation()
        {
            if (count <= 0)
            {
                res = [0];
                return;
            }

            res = new long[count];
            res[0] = 0;

            if (count == 1) return;

            res[1] = 1;
            for (int i = 2; i < count; i++)
            {
                res[i] = res[i - 1] + res[i - 2];
            }
        }
    }
}