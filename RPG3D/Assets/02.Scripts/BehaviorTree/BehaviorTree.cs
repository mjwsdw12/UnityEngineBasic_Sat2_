using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BT
{
    public enum Status
    {
        Success,
        Failure,
        Running
    }

    public class BTTester
    {
        BehaviorTree bt;
        void TestBuild()
        {
            bt = new BehaviorTree();

            bt.root = new Root();
            Sequence sequence1 = new Sequence();
            Selector selecotor1 = new Selector();
            Condition condition1 = new Condition(() => true);
            Execution execution1 = new Execution(() => Status.Success);
            Execution execution2 = new Execution(() => Status.Success);
            Execution execution3 = new Execution(() => Status.Success);
            Execution execution4 = new Execution(() => Status.Success);

            bt.root.child = sequence1;
            sequence1.AddChild(execution1);
            sequence1.AddChild(condition1);
            sequence1.AddChild(selecotor1);
            condition1.child = execution2;
            selecotor1.AddChild(execution3);
            selecotor1.AddChild(execution4);

            bt.StartBuild()
                .Sequence()
                    .Sequence()
                        .Execution(() => Status.Success)
                        .Execution(() => Status.Success)
                    .ExitCurrentComposite()
                    .Sequence()
                        .Execution(() => Status.Success);

        }

        void TestTick()
        {
            bt.Tick();
        }
    }

    public class BehaviorTree
    {
        public Root root;

        public Status Tick()
        {
            return root.Invoke();
        }

        // build
        //=========================================================
        private Behavior _current;
        private Stack<Composite> _compositeStack = new Stack<Composite>();
        public BehaviorTree StartBuild()
        {
            root = new Root();
            _current = root;
            return this;
        }

        public BehaviorTree Sequence()
        {
            Sequence sequence = new Sequence();
            AttachAsChild(_current, sequence);
            _compositeStack.Push(sequence);
            _current = sequence;
            return this;
        }

        public BehaviorTree RandomSequence()
        {
            RandomSequence sequence = new RandomSequence();
            AttachAsChild(_current, sequence);
            _compositeStack.Push(sequence);
            _current = sequence;
            return this;
        }

        public BehaviorTree Selector()
        {
            Selector selector = new Selector();
            AttachAsChild(_current, selector);
            _compositeStack.Push(selector);
            _current = selector;
            return this;
        }
        public BehaviorTree RandomSelector()
        {
            RandomSelector selector = new RandomSelector();
            AttachAsChild(_current, selector);
            _compositeStack.Push(selector);
            _current = selector;
            return this;
        }

        public BehaviorTree Condition(Func<bool> func)
        {
            Condition condition = new Condition(func);
            AttachAsChild(_current, condition);
            _current = condition;
            return this;
        }

        public BehaviorTree Parallel(Parallel.Policy successPolicy, Parallel.Policy failurePolicy)
        {
            Parallel parallel = new Parallel(successPolicy, failurePolicy);
            AttachAsChild(_current, parallel);
            _compositeStack.Push(parallel);
            _current = parallel;
            return this;
        }

        public BehaviorTree Execution(Func<Status> execute)
        {
            Execution execution = new Execution(execute);
            AttachAsChild(_current, execution);

            if (_compositeStack.Count > 0)
                _current = _compositeStack.Peek();
            else
                _current = null;

            return this;
        }

        public BehaviorTree RandomSleep(float minTime, float maxTime)
        {
            RandomSleep randomSleep = new RandomSleep(minTime, maxTime);
            AttachAsChild(_current, randomSleep);
            _current = randomSleep;
            return this;
        }

        public BehaviorTree ExitCurrentComposite()
        {
            if (_compositeStack.Count > 1)
            {
                _compositeStack.Pop();
                _current = _compositeStack.Peek();
            }
            else if (_compositeStack.Count == 1)
            {
                _compositeStack.Pop();
                _current = null;
            }
            else
            {
                throw new Exception("[BehaviorTree] : Composite stack does not exist");
            }

            return this;
        }

        private void AttachAsChild(Behavior parent, Behavior child)
        {
            if (parent is Composite)
            {
                (parent as Composite).AddChild(child);
            }
            else if (parent is IChild)
            {
                (parent as IChild).child = child;
            }
            else
            {
                throw new Exception("[BehaviorTree] : 자식이 없는 부모행동에 자식을 추가하려는 시도가 일어남");
            }
        }

    }

    public abstract class Behavior
    {
        public abstract Status Invoke(out Behavior leaf);
    }

    public interface IChild
    {
        Behavior child { get; set; }
    }

    public class Root : Behavior, IChild
    {
        public Behavior child { get; set; }
        public Behavior runningLeaf { get; set; }

        public Status Invoke()
        {
            if (runningLeaf == null)
                return Invoke(out Behavior leaf);
            else
                return InvokeRunningLeaf();
        }

        public override Status Invoke(out Behavior leaf)
        {
            Status tmpStatus = child.Invoke(out leaf);

            if (tmpStatus == Status.Running)
                runningLeaf = leaf;
            else
                runningLeaf = null;

            return tmpStatus;
        }

        public Status InvokeRunningLeaf()
        {
            Status tmpStatus = runningLeaf.Invoke(out Behavior leaf);

            if (tmpStatus == Status.Running)
                runningLeaf = leaf;
            else
                runningLeaf = null;

            return tmpStatus;
        }
    }

    public class Condition : Behavior, IChild
    {
        public Behavior child { get; set; }
        private event Func<bool> _condition;

        public Condition(Func<bool> condition)
        {
            _condition = condition;
        }

        public override Status Invoke(out Behavior leaf)
        {
            leaf = null;
            if (_condition.Invoke())
                return child.Invoke(out leaf);
            else
                return Status.Failure;
        }
    }

    public class Execution : Behavior
    {
        public event Func<Status> _execute;

        public Execution(Func<Status> execute)
        {
            _execute = execute;
        }

        public override Status Invoke(out Behavior leaf)
        {
            leaf = this;
            return _execute.Invoke();
        }
    }

    public abstract class Decorator : Behavior, IChild
    {
        public Behavior child { get; set; }
        public override Status Invoke(out Behavior leaf)
        {
            return Decorate(child.Invoke(out leaf), out leaf);
        }

        public abstract Status Decorate(Status status, out Behavior leaf);
    }

    public abstract class Composite : Behavior
    {
        public List<Behavior> children { get; set; }

        public Composite()
        {
            children = new List<Behavior>();
        }

        public void AddChild(Behavior child) => children.Add(child);
    }

    /// <summary>
    /// 자식들을 일련의 과정으로 처리하는 행동.
    /// 모든 자식이 success 를 반환하면 success 를 반환함.
    /// </summary>
    public class Sequence : Composite
    {
        public override Status Invoke(out Behavior leaf)
        {
            leaf = this;
            Status result = Status.Failure;

            foreach (Behavior child in children)
            {
                result = child.Invoke(out leaf);

                if (result != Status.Success)
                {
                    leaf = child;
                    return result;
                }
            }

            return Status.Success;
        }
    }

    public class RandomSequence : Composite
    {
        public override Status Invoke(out Behavior leaf)
        {
            leaf = this;
            Status result = Status.Failure;

            //foreach (Behavior child in children.OrderBy(c => UnityEngine.Random.Range(0, children.Count)))
            foreach (Behavior child in children.OrderBy(c => Guid.NewGuid()))
            {
                result = child.Invoke(out leaf);

                if (result != Status.Success)
                {
                    leaf = child;
                    return result;
                }
            }

            return Status.Success;
        }
    }

    public class Filter : Sequence
    {
        public void AddCondition(Condition condition) => children.Insert(0, condition);
    }

    /// <summary>
    /// 성공한 자식을 선택하는 행동
    /// 모든 자식이 failure 를 반환하면 failure 를 반환함.
    /// success/running 반환시 자식 순회를 중단하고 결과를 반환함.
    /// </summary>
    public class Selector : Composite
    {
        public override Status Invoke(out Behavior leaf)
        {
            leaf = this;
            Status result = Status.Failure;

            foreach (Behavior child in children)
            {
                result = child.Invoke(out leaf);

                if (result != Status.Failure)
                {
                    leaf = child;
                    return result;
                }
            }

            return Status.Failure;
        }
    }

    public class RandomSelector : Composite
    {
        public override Status Invoke(out Behavior leaf)
        {
            leaf = this;
            Status result = Status.Failure;

            foreach (Behavior child in children.OrderBy(c => Guid.NewGuid()))
            {
                result = child.Invoke(out leaf);

                if (result != Status.Failure)
                {
                    leaf = child;
                    return result;
                }
            }

            return Status.Failure;
        }
    }

    /// <summary>
    /// 자식행동 결과 상관없이 모든 자식 행동 수행함.
    /// 반환값은 반환 정책에 따라서 결정됨.
    /// </summary>
    public class Parallel : Composite
    {
        public enum Policy
        {
            RequireOne,
            RequireAll,
        }

        private Policy _successPolicy;
        private Policy _failurePolicy;

        public Parallel(Policy successPolicy, Policy failurePolicy)
        {
            _successPolicy = successPolicy;
            _failurePolicy = failurePolicy;
        }

        public override Status Invoke(out Behavior leaf)
        {
            leaf = this;
            Behavior runningLeaf = null;
            int successCount = 0;
            int failureCount = 0;

            Status result = Status.Failure;
            foreach (Behavior child in children)
            {
                result = child.Invoke(out leaf);

                switch (result)
                {
                    case Status.Success:
                        successCount++;
                        break;
                    case Status.Failure:
                        failureCount++;
                        break;
                    case Status.Running:
                        runningLeaf = leaf;
                        break;
                    default:
                        throw new Exception("[Pararell Behavior] : 행동 반환값 오류");
                }
            }

            if (runningLeaf != null)
            {
                leaf = runningLeaf;
                return Status.Running;
            }
            else if ((_successPolicy == Policy.RequireOne && successCount >= 1) ||
                (_successPolicy == Policy.RequireAll && successCount >= children.Count))
            {
                return Status.Success;
            }
            else if ((_failurePolicy == Policy.RequireOne && failureCount >= 1) ||
                     (_failurePolicy == Policy.RequireAll && failureCount >= children.Count))
            {
                return Status.Failure;
            }
            else
            {
                throw new Exception("[Pararell Behavior] : 반환 정책 오류");
            }
        }
    }

    public class RandomSleep : Decorator
    {
        private enum Step
        {
            BeginSleep,
            Sleeping,
            EndSleep
        }
        private Step _step;
        private float _timeMin, _timeMax, _timeMark, _duration;

        public RandomSleep(float timeMin, float timeMax) : base()
        {
            _timeMin = timeMin;
            _timeMax = timeMax;
        }

        public override Status Invoke(out Behavior leaf)
        {
            if (_step == Step.BeginSleep)
                return base.Invoke(out leaf);
            else
                return Decorate(Status.Success, out leaf);
        }

        public override Status Decorate(Status status, out Behavior leaf)
        {
            leaf = this;

            if (status == Status.Failure)
                return Status.Failure;

            Status result = Status.Running;

            switch (_step)
            {
                case Step.BeginSleep:
                    {
                        _timeMark = Time.time;
                        _duration = Random.Range(_timeMin, _timeMax);
                        _step++;
                    }
                    break;
                case Step.Sleeping:
                    {
                        if (Time.time - _timeMark > _duration)
                            _step++;
                    }
                    break;
                case Step.EndSleep:
                    {
                        result = Status.Success;
                        _step = Step.BeginSleep;
                    }
                    break;
                default:
                    break;
            }

            return result;
        }
    }
}
