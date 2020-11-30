using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace luigi.algorithms
{
    public class AStar<T>
    {
        private List<State> _openStates;
        private List<State> _closeStates;

        private Func<T, Dictionary<T, double>> _nextStepsRule;
        private Func<T, T, double> _srcCostFunc;
        private Func<T, T, double> _dstCostFunc;

        private ILog _log;

        public T InitialState { get; private set; }
        public T FinalState { get; private set; }
        public List<T> Steps { get ; private set; }
        public int NSteps { get; private set; }
        public double Cost { get; private set; }

        public AStar(T initialState, T finalState)
        {
            InitialState = initialState;
            FinalState = finalState;
        }

        public void SetNextStepsRule(Func<T, Dictionary<T, double>> nextStepsRule)
        {
            _nextStepsRule = nextStepsRule;
        }

        public void SetCostFunction(Func<T, T, double> srcCostFunc = null, Func<T, T, double> dstCostFunc = null)
        {
            _srcCostFunc = srcCostFunc;
            _dstCostFunc = dstCostFunc;
        }

        public void SetLogger(ILog log)
        {
            _log = log;
        }

        public void Run()
        {
            Reset();

            bool rs = false;

            _openStates.Add(new State(InitialState, 0, 0, null));
            while (_openStates.Count > 0 && !rs)
            {
                _openStates.Sort();
                var state = _openStates.First();
                if (state.Value.Equals(FinalState))
                {
                    State step = state;
                    while (step.Parent != null)
                    {
                        Steps.Insert(0, step.Value);
                        NSteps++;
                        step = step.Parent;
                    }
                    //Steps.Insert(0, step.Value);
                    rs = true;
                    return;
                }
                else
                {
                    //Console.WriteLine(_openStates.Count + "\t" + state.Priority);
                    _openStates.Remove(state);
                    _closeStates.Add(state);
                    Parallel.ForEach(_nextStepsRule(state.Value), nextStep =>
                    {
                        if (_closeStates.Find(e => object.Equals(e.Value, nextStep.Key)) == null)
                        {
                            double priority = 0;
                            double srcPriority = 0;
                            if (_srcCostFunc == null)
                            {
                                srcPriority = state.SrcPriority + nextStep.Value;
                            }
                            else
                            {
                                priority = _srcCostFunc(InitialState, nextStep.Key);
                            }
                            if (_dstCostFunc != null)
                            {
                                priority = srcPriority + _dstCostFunc(nextStep.Key, FinalState);
                            }

                            _openStates.Add(new State(nextStep.Key, priority, srcPriority, state));
                        }
                    });
                }
            }
        }

        private void Reset()
        {
            _openStates = new List<State>();
            _closeStates = new List<State>();

            Cost = 0;
            NSteps = 0;
            Steps = new List<T>();
        }

        private class State: IComparable<State>
        {
            public T Value;
            public double Priority;
            public double SrcPriority;
            public State Parent;

            public State(T value, double priority, double srcPriority, State parent)
            {
                Value = value;
                Priority = priority;
                SrcPriority = srcPriority;
                Parent = parent;
            }

            public int CompareTo(State other)
            {
                if(other != null)
                    return Priority.CompareTo(other.Priority);
                return -1;
            }
        }
    }
}
