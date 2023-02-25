﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ClassScheduler.Domain.Users;

public abstract class UserBase
{
    protected UserBase(Guid id, string firstName, string lastName, string email)
    {
        ValidateConstructorArguments(id, firstName, lastName, email);

        Email = email;
        Id = id;
        FirstName = firstName;
        LastName = lastName;
    }

    protected UserBase(string firstName, string lastName, string email)
    {
        ValidateConstructorArguments(firstName, lastName, email);

        Email = email;
        FirstName = firstName;
        LastName = lastName;
    }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public Guid Id { get; set; }
    
    public void UpdateFirstName(string firstName)
    {
        FirstName = firstName;
    }

    public void UpdateLastName(string lastName)
    {
        LastName = lastName;
    }

    public void UpdateEmail(string email)
    {
        if (!IsValidEmail(email))
        {
            throw new ArgumentException("Invalid email address", nameof(email));
        }

        Email = email;
    }

    private static bool IsValidEmail(string email)
    {
        // regular expression pattern to match email format
        string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

        return Regex.IsMatch(email, pattern);
    }

    private static void ValidateConstructorArguments(string firstName, string lastName, string email)
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
            throw new ArgumentException("First name cannot be empty", nameof(firstName));
        }

        if (string.IsNullOrWhiteSpace(lastName))
        {
            throw new ArgumentException("Last name cannot be empty", nameof(lastName));
        }

        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("Email cannot be empty", nameof(email));
        }

        if (!IsValidEmail(email))
        {
            throw new ArgumentException("Invalid email address", nameof(email));
        }
    }

    private static void ValidateConstructorArguments(Guid id, string firstName, string lastName, string email)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("Id cannot be empty", nameof(id));
        }

        ValidateConstructorArguments(firstName, lastName, email);
    }
}
