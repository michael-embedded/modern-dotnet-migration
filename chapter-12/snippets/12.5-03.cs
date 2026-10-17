// Modern .NET, Chapter 12, Section 12.5
using System.Text;
using Microsoft.Extensions.ObjectPool;

var pool = new DefaultObjectPoolProvider().CreateStringBuilderPool();

static string Render(ObjectPool<StringBuilder> pool, int id, string name)
{
    var sb = pool.Get();
    try
    {
        sb.Append("Order #").Append(id).Append(" for ").Append(name);
        return sb.ToString();
    }
    finally
    {
        pool.Return(sb);                    // the policy clears the builder
    }
}

Console.WriteLine(Render(pool, 7, "Ada"));
Console.WriteLine(Render(pool, 8, "Grace"));
