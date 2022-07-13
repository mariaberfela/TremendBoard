# TremendBoard

##Transient:

timeService1: 02:30:58.298

timeService2: 02:30:58.311

We notice different values in the same instance.

## Scoped

-- Attempt 1:

timeService1: 02:34:25.725

timeService2: 02:34:25.725

-- Attempt 2:

timeService1: 02:34:49.581

timeService2: 02:34:49.581

We notice the same value during the same instance, but different if we refresh the page.

## Singleton

-- Attempt 1:

timeService1: 02:36:53.766

timeService2: 02:36:53.766

--Attempt 2:

timeService1: 02:36:53.766

timeService2: 02:36:53.766

Same value no matter what.