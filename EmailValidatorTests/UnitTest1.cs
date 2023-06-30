using EmailValidator;

namespace EmailValidatorTests;

public class UnitTest1
{
    [Theory]
    [InlineData("a@b.c")]
    [InlineData("email@example.com")]
    [InlineData("EMAIL@EXAMPLE.COM")]
    [InlineData("firstname.lastname@example.com")]
    [InlineData("email@subdomain.example.com")]
    [InlineData("firstname+lastname@example.com")]
    [InlineData("email@example-one.com")]
    [InlineData("email@example.name")]
    [InlineData("email@example.museum")]
    [InlineData("email@example.co.jp")]
    [InlineData("firstname-lastname@example.com")]
    [InlineData("foo@bar.br")]
    [InlineData("bar@microsoft.com")]
    [InlineData("foo@a123456789.a12")]
    [InlineData("bar@a123456789-a123456789-a123456789-a123456789-a123456789KK.com.br")]
    [InlineData("foo@foo.bar.qux.nox")]
    [InlineData("a123456789a123456789a123456789a123456789a123456789a123456789abcd@foo.bar.qux.nox")]
    [InlineData("a123456789a123456789a123456789a123456789a123456789a123456789abcd@foo.012345678901234567890123456789012345678901234567bar.qux.nox")]
    [InlineData("todoscaracteresvalidos!#$%&'*+/=?^_`{|}@shubiduba.com")]    
    public void EmailIsWellFormed(string sut)
    {
        // Arrange
        // Act
        var isWellFormed = EmailValidatorFast.IsWellFormed(sut);
        // Assert
        Assert.True(isWellFormed);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData(" ")]
    [InlineData("plainaddress")]
    [InlineData("user@example")]
    [InlineData("user@br")]
    [InlineData("#@%^%#$@#$@#.com")]
    [InlineData("alguns(invalidos)@shubiduba.com")]
    [InlineData("@example.com")]
    [InlineData(" @example.com")]
    [InlineData("email@.com")]
    [InlineData("Joe Smith <email@example.com>")]
    [InlineData("email.example.com")]
    [InlineData("email@example@example.com")]
    [InlineData(".email@example.com")]
    [InlineData("email.@example.com")]
    [InlineData("email..email@example.com")]
    [InlineData("あいうえお@example.com")]
    [InlineData("email@example.com (Joe Smith)")]
    [InlineData("email@example")]
    [InlineData("email@-example.com")]
    [InlineData("email@111.222.333.44444")]
    [InlineData("email@example..com")]
    [InlineData("Abc..123@example.com")]
    [InlineData("Abc..123@exa--mple.com")]
    [InlineData("Abc--123@example.com")]
    [InlineData("..Abc123@example.com")]
    [InlineData("--Abc123@example.com")]
    [InlineData("..Abc--123@example.com")]
    [InlineData("--Abc..123@example.com")]    
    [InlineData("foo@aa")]
    [InlineData("foo@a123456789.a123456789.a123456789.a123456789.a123456789.a123456789")]
    [InlineData("foo@domain-")]
    [InlineData("bar@-domain")]
    [InlineData("foo@-DOMAIN")]
    [InlineData("bar@dom$in")]
    [InlineData("foo@.domain")]
    [InlineData("bar@domain.")]
    [InlineData("foo-@domain")]
    [InlineData("-bar@domain")]
    [InlineData("-foo@DOMAIN")]
    [InlineData(".foo@domain")]
    [InlineData("bar.@domain")]
    [InlineData("foobar@")]
    [InlineData("email@")]    
    [InlineData("email@ ")]
    [InlineData("email@.")]
    [InlineData("email@-")]
    public void EmailIsInvalid(string sut)
    {
        // Arrange
        // Act
        var isWellFormedFast = EmailValidatorFast.IsWellFormed(sut);
        // Assert
        Assert.False(isWellFormedFast);
    }
}