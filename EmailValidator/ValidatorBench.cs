using BenchmarkDotNet.Attributes;

namespace EmailValidator;

[MemoryDiagnoser]
public class ValidatorBench
{
    private List<string?> _cases = new()
    {
        "a@b.c",
        "email@example.com",
        "EMAIL@EXAMPLE.COM",
        "firstname.lastname@example.com",
        "email@subdomain.example.com",
        "firstname+lastname@example.com",
        "email@example-one.com",
        "email@example.name",
        "email@example.museum",
        "email@example.co.jp",
        "firstname-lastname@example.com",
        "foo@bar.br",
        "bar@microsoft.com",
        "foo@a123456789.a12",
        "bar@a123456789-a123456789-a123456789-a123456789-a123456789KK.com.br",
        "foo@foo.bar.qux.nox",
        "a123456789a123456789a123456789a123456789a123456789a123456789abcd@foo.bar.qux.nox",
        "todoscaracteresvalidos!#$%&'*+/=?^_`{|}@shubiduba.com",
        "",
        null,
        " ",
        "plainaddress",
        "user@example",
        "user@br",
        "#@%^%#$@#$@#.com",
        "alguns(invalidos)@shubiduba.com",
        "@example.com",
        "email@.com",
        "Joe Smith <email@example.com>",
        "email.example.com",
        "email@example@example.com",
        ".email@example.com",
        "email.@example.com",
        "email..email@example.com",
        "あいうえお@example.com",
        "email@example.com (Joe Smith)",
        "email@example",
        "email@-example.com",
        "email@111.222.333.44444",
        "email@example..com",
        "Abc..123@example.com",
        "foo@aa",
        "foo@a123456789.a123456789.a123456789.a123456789.a123456789.a123456789",
        "foo@domain-",
        "bar@-domain",
        "foo@-DOMAIN",
        "bar@dom$in",
        "foo@.domain",
        "bar@domain.",
        "foo-@domain",
        "-bar@domain",
        "-foo@DOMAIN",
        ".foo@domain",
        "bar.@domain",
        "foobar@",
        "email@"
    };

    [Benchmark]
    public void Modern()
    {
        for (var i = 0; i < _cases.Count; i++)
        {
            var _ = EmailValidatorFast.IsWellFormed(_cases[i]!);
        }
    }
}