using System;
using System.Collections.Generic;
using System.Linq;

namespace Everest.Pipeline
{
    public class PipelineOptions
    {
        private readonly IEnumerable<PipelineOption> _options;
        private readonly HashSet<PipelineOption> _unusedOptions;

        public PipelineOptions(IEnumerable<PipelineOption> options)
        {
            _options = options;
            _unusedOptions = new HashSet<PipelineOption>(_options);
        }

        public void AssertAllOptionsWereUsed()
        {
            if (_unusedOptions.Count > 0)
                throw new UnsupportedOptionException(_unusedOptions.First());
        }

        public void Use<TOption>(Action<TOption> useOption) where TOption : PipelineOption
        {
            foreach (var option in _options.OfType<TOption>())
            {
                useOption(option);
                _unusedOptions.Remove(option);
            }
        }
    }
}