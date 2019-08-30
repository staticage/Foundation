using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Foundation.Workflow
{
    public class WorkflowInstance : IWorkflowVariable
    {
        public Guid Id { get; set; }
        public List<ExecutionPointer> ExecutionPointers { get; set; } = new List<ExecutionPointer>();
        public WorkflowStatus Status { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string WorkflowDefinitionName { get; set; }
        public int Version { get; set; }

        private WorkflowInstance()
        {
        }

        public static WorkflowInstance Start(Guid id, WorkflowDefinition definition)
        {
            var workflow = new WorkflowInstance
            {
                Id = id,
                StartTime = DateTime.Now,
                WorkflowDefinitionName = definition.Name,
                Version = definition.Version
            };
            return workflow;
        }

        public ExecutionPointer StartStep(string stepId)
        {
            var executionPointer = new ExecutionPointer
            {
                Id = Guid.NewGuid(),
                StartTime = DateTime.Now,
                StepId = stepId,
                Status = PointerStatus.Running,
            };

            ExecutionPointers.Add(executionPointer);
            return executionPointer;
        }

        public ExecutionPointer GetNextExecutionPointer()
        {
            return ExecutionPointers.LastOrDefault();
        }

        public void Complete()
        {
            if (Status != WorkflowStatus.Running)
            {
                throw new InvalidOperationException("Cannot complete a no-running workflow");
            }

            EndTime = DateTime.Now;
            Status = WorkflowStatus.Complete;
        }

        public ExecutionResult Obsolete()
        {
            if (Status != WorkflowStatus.Running)
            {
                throw new InvalidOperationException("Cannot obsolete a no-running workflow");
            }
            EndTime = DateTime.Now;
            Status = WorkflowStatus.Terminated;
            return ExecutionResult.Next();
        }

        public bool TryGetVariable(string name, out object value)
        {
            throw new NotImplementedException();
        }

        public void SetVariable(string name, object value)
        {
            throw new NotImplementedException();
        }
    }

    public class WorkflowVariables<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private IDictionary<TKey, TValue> _innerDictionary;
        private bool _isChanged = false;
        public WorkflowVariables() : this(new Dictionary<TKey, TValue>())
        {
        }

        public WorkflowVariables(IDictionary<TKey, TValue> values)
        {
            _innerDictionary = new Dictionary<TKey, TValue>(values);
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => _innerDictionary.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _innerDictionary.GetEnumerator();

        private WorkflowVariables<TKey, TValue> Do(Action<WorkflowVariables<TKey, TValue>> action)
        {
            var variables = this;
            if (!_isChanged)
            {
                variables = new WorkflowVariables<TKey, TValue>(_innerDictionary);
                variables._isChanged = true;
            }
            action(variables);
            return variables;
        }
        


        public void Add(KeyValuePair<TKey, TValue> item) => Do(x => x._innerDictionary.Add(item));

        public void Clear() => Do(x => x._innerDictionary.Clear());

        public bool Contains(KeyValuePair<TKey, TValue> item) => _innerDictionary.Contains(item);

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) =>
            _innerDictionary.CopyTo(array, arrayIndex);

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public int Count { get; }
        public bool IsReadOnly { get; }
        public void Add(TKey key, TValue value)
        {
            throw new NotImplementedException();
        }

        public bool ContainsKey(TKey key)
        {
            throw new NotImplementedException();
        }

        public bool Remove(TKey key)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            throw new NotImplementedException();
        }

        public TValue this[TKey key]
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public ICollection<TKey> Keys { get; }
        public ICollection<TValue> Values { get; }
    }
}