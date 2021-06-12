using System;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using Sandbox.Shared;

namespace Sandbox.Facts
{
    public static class Extensions
    {
        //--------------------------------------------------
        public static int NextId([NotNull] this Model model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return model.Facts.Count + 1;
        }

        //--------------------------------------------------
        public static Model InsertFact([NotNull] this Model model, [NotNull] Fact fact)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return Model.InsertFact(model, fact);
        }

        //--------------------------------------------------
        public static string ToGraphviz([NotNull] this Model model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var builder = new StringBuilder();
            builder.AppendLine("digraph {");
            foreach (var r in model.Facts.OfType<Restaurant>())
            {
                builder.Append(r.Id);
                builder.Append(" [");
                builder.Append("label=\"");
                builder.Append(r.Name.Value);
                builder.Append("\"]");
                builder.AppendLine(";");
            }

            foreach (var t in model.Facts.OfType<Table>())
            {
                builder.Append(t.Id);
                builder.Append(" [");
                builder.Append("label=\"");
                builder.Append($"Table:{t.Number}");
                builder.Append("\"]");
                builder.AppendLine(";");

                builder.Append(t.Id + "->" + t.Restaurant.Id);
            }

            builder.AppendLine("}");
            return builder.ToString();

        }

        public static Restaurant GetRestaurant([NotNull] this Model model, string name)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return model.Facts.OfType<Restaurant>().FirstOrDefault(r => r.Name.Value.Equals(name));
        }

    }
}