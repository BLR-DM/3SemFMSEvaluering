﻿namespace FMSEvaluering.Domain.Values.DataServer;

public record ModelClassValue
{
    public string Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<TeacherSubjectValue> TeacherSubjects { get; set; }
}