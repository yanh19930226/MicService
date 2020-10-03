using Core.Data.Domain.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Data.Domain.Models
{
	public abstract class Entity : Entity<long>
	{
	}
	public abstract class Entity<T>
	{
		private List<Event> _domainEvents;

		public T Id
		{
			get;
			protected set;
		}

		public DateTime CreateTime
		{
			get;
			set;
		} 

		public DateTime ModifyTime
		{
			get;
			set;
		} 

		public bool IsDel
		{
			get;
			set;
		}

		[NotMapped]
		public IReadOnlyCollection<Event> DomainEvents => _domainEvents?.AsReadOnly();

		public void AddDomainEvent(Event eventItem)
		{
			_domainEvents = (_domainEvents ?? new List<Event>());
			_domainEvents.Add(eventItem);
		}

		public void RemoveDomainEvent(Event eventItem)
		{
			_domainEvents?.Remove(eventItem);
		}

		public void ClearDomainEvents()
		{
			_domainEvents?.Clear();
		}

		public override bool Equals(object obj)
		{
			Entity<T> entity = obj as Entity<T>;
			if ((object)this == entity)
			{
				return true;
			}
			if ((object)entity == null)
			{
				return false;
			}
			return Id.Equals(entity.Id);
		}

		public static bool operator ==(Entity<T> a, Entity<T> b)
		{
			if ((object)a == null && (object)b == null)
			{
				return true;
			}
			if ((object)a == null || (object)b == null)
			{
				return false;
			}
			return a.Equals(b);
		}

		public static bool operator !=(Entity<T> a, Entity<T> b)
		{
			return !(a == b);
		}

		public override int GetHashCode()
		{
			return GetType().GetHashCode() * 907 + Id.GetHashCode();
		}

		public override string ToString()
		{
			return GetType().Name + " [Id=" + Id?.ToString() + "]";
		}
	}
}
