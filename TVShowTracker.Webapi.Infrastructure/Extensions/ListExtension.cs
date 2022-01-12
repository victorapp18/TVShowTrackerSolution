using System;
using System.Collections.Generic;
using System.Linq;

namespace TVShowTracker.Webapi.Infrastructure.Extensions
{
    public static class ListExtension
    {
        public static IDictionary<int, List<TOut>> ToPageResult<TIn, TOut>(this List<TIn> input, Func<TIn, TOut> mapper, int blockSize = 500) 
        {
            IDictionary<int, List<TOut>> result = new Dictionary<int, List<TOut>>();

            if (input == null || input.Count == 0)
                return result;

            int blocksCount = (int) Math.Ceiling((decimal)input.Count / blockSize);

            for (int i = 0; i <= blocksCount; i++)
            {
                List<TOut> block = input.Skip(i * blockSize)
                                        .Take(blockSize)
                                        .Select(mapper)
                                        .ToList();

                result.Add(i, block);
            }

            return result;
        }


        public static IDictionary<int, List<TEntity>> ToPageResult<TEntity>(this List<TEntity> input, int blockSize = 500)
        {
            IDictionary<int, List<TEntity>> result = new Dictionary<int, List<TEntity>>();

            if (input == null || input.Count == 0)
                return result;

            int blocksCount = (int)Math.Ceiling((decimal)input.Count / blockSize);

            for (int i = 0; i <= blocksCount; i++)
            {
                List<TEntity> block = input.Skip(i * blockSize)
                                           .Take(blockSize)
                                           .ToList();
                if (block.Any())
                    result.Add(i, block);
            }

            return result;
        }

       
    }
}
