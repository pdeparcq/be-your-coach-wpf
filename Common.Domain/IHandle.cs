﻿namespace Common.Domain
{
    public interface IHandle<T> where T:IDomainEvent
    {
        void Handle(T domainEvent);
    }
}
