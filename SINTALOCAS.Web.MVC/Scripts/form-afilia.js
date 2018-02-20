function validarFormAfilia() {

    var formData = $("#formAfilia").serialize();

    $.ajax({
        type: "POST",
        data: formData,
        url: "/Afiliacao/ValidarFormJSON",
        dataType: 'json',
        // contentType: false,
        // processData: false,               
        success: function (response) {
            
            //alert(response.msg + ' - ' + response.success);
            $("#rForm").html(response.msg);
            //return response.success;

            if (response.success) {
                $("#formAfilia").submit();
            } else {
                alert(response.msg);
            }

        },
        error: function (response) {
            // alert("Ocorreu um erro durante o processo, tente novamente!");   
            alert("Ocorreu um erro durante o processo, tente novamente!\n retorno: " + response.status); 
        }
    });
}

function validarCPF(valor) 
{
    var url = "/Afiliacao/ValidarCPF/";
    var valor = $(valor).val();
    $.get(url, { Cpf: valor}, function (data)
    {
        $("#rCpf").html(data);
    });
}

function validarCNPJ(valor)
{
    var url = "/Afiliacao/ValidarCNPJ/";
    var valor = $(valor).val();
    $.get(url, { Cnpj: valor}, function (data)
    {
        $("#rCnpj").html(data);
    });
}

function validarPIS(valor) {

    var url = "/Afiliacao/ValidarPIS/";
    var valor = $(valor).val();
    $.get(url, { Pis: valor}, function (data)
    {
        $("#rPis").html(data);
    });

}

function validarRG(valor) {

    var url = "/Afiliacao/ValidarRG/";
    var valor = $(valor).val();
    $.get(url, { Rg: valor}, function (data)
    {
        $("#rRg").html(data);
    });

}

function validarCEP(valor) {

    $("#rCEP").html("");
    var url = "/Afiliacao/ValidarCEP/";
    var valor = $(valor).val();
    var msgErro = "CEP inválido";

    if (valor.length < 8) 
    {
        $("#rCEP").html(msgErro);
        //alert(msgErro);
    }
    else
    {
        $.getJSON(url, { Cep: valor}, function (data)
        {
            $("#Bairro").val(data.Bairro);
            $("#Rua").val(data.Logradouro);
            $("#Cidade").val(data.Cidade);
            $("#UF").val(data.UF);
            $("#CEP").val(data.CEP);
    
            if (data == null) $("#rCEP").html(msgErro); //alert(msgErro);
    
        });
    }

}

function validarUF(valor) 
{
    var url = "/Afiliacao/ValidarUF/";
    var valor = $(valor).val();
    $.get(url, { Uf: valor}, function (data)
    {
        $("#rUF").html(data);
    });
}

// MASCARA NOS CAMPOS
$(document).ready(function(){
    // $("#DtNasc").mask("(99) 9999-9999");
    $("#TelCelNum").mask("99999-9999");
    $("#TelResNum").mask("9999-9999");
    $("#DtNasc").mask("99/99/9999");
    $("#Rg").mask("9999999999");
    $("#Cpf").mask("999999999999");
    $("#Cnpj").mask("999999999999999");
    $("#CEP").mask("99999999");
});

function validarEmail(valor) 
{
    var url = "/Afiliacao/ValidarEMAIL/";
    var valor = $(valor).val();
    $.get(url, { emailtx: valor}, function (data)
    {
        $("#rEmail").html(data);
    });
}

function validarDtNasc(valor) 
{
    var url = "/Afiliacao/ValidarDtNasc/";
    var valor = $(valor).val();
    $.get(url, { dtnasc: valor}, function (data)
    {
        $("#rDtNasc").html(data);
    });
}

