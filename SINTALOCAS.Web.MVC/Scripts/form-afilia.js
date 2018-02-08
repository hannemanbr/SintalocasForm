$('#Cpf').blur(function ()
{
    var url = "/Afiliacao/ValidarCPF/";
    var valor = $(this).val();
    $.get(url, { Cpf: valor}, function (data)
    {
        $("#rCpf").html(data);
    });
})

$('#Cnpj').blur(function ()
{
    var url = "/Afiliacao/ValidarCNPJ/";
    var valor = $(this).val();
    $.get(url, { Cnpj: valor}, function (data)
    {
        $("#rCnpj").html(data);
    });
})

$('#Pis').blur(function ()
{
    var url = "/Afiliacao/ValidarPIS/";
    var valor = $(this).val();
    $.get(url, { Pis: valor}, function (data)
    {
        $("#rPis").html(data);
    });
})

$('#CEP').blur(function ()
{
    var url = "/Afiliacao/ValidarCEP/";
    var valor = $(this).val();
    $.getJSON(url, { Cep: valor}, function (data)
    {
        $("#Bairro").val(data.Bairro);
        $("#Rua").val(data.Rua);
        $("#Cidade").val(data.Cidade);
        $("#UF").val(data.UF);
    });
})

$('#UF').blur(function ()
{
    var url = "/Afiliacao/ValidarUF/";
    var valor = $(this).val();
    $.get(url, { Uf: valor}, function (data)
    {
        $("#rUF").html(data);
    });
})

