function validarFormAfilia(rootView) {

    var formData = $("#formAfilia").serialize();

    $.ajax({
        type: "POST",
        data: formData,
        url: rootView + "/ValidarFormJSON",
        dataType: 'json',
        // contentType: false,
        // processData: false,               
        success: function (response) {

            //alert(response.msg + ' - ' + response.success);
            $("#rForm").html(response.msg);
            //return response.success;

            if (response.success) {
                //$("#formAfilia").submit();
                window.location.replace("Dependentes");
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

function validarCPF(valor, rootView) {
    var url = rootView + "/ValidarCPF/";
    var valor = $(valor).val();
    $.get(url, { Cpf: valor }, function (data) {
        $("#rCpf").html(data);
    });
}

function validarCNPJ(valor, rootView) {
    var url = rootView + "/ValidarCNPJ/";
    var valor = $(valor).val();
    $.get(url, { Cnpj: valor }, function (data) {
        $("#rCnpj").html(data);
    });
}

function validarPIS(valor, rootView) {

    var url = rootView + "/ValidarPIS/";
    var valor = $(valor).val();
    $.get(url, { Pis: valor }, function (data) {
        $("#rPis").html(data);
    });

}

function validarRG(valor, rootView) {

    var url = rootView + "/ValidarRG/";
    var valor = $(valor).val();
    $.get(url, { Rg: valor }, function (data) {
        $("#rRg").html(data);
    });

}

function validarCEP(valor, rootView) {

    $("#rCEP").html("");
    var url = rootView + "/ValidarCEP/";
    var valor = $(valor).val();
    var msgErro = "CEP inválido";

    if (valor.length < 8) {
        $("#rCEP").html(msgErro);
        //alert(msgErro);
    }
    else {
        $.getJSON(url, { Cep: valor }, function (data) {
            $("#Bairro").val(data.Bairro);
            $("#Rua").val(data.Logradouro);
            $("#Cidade").val(data.Cidade);
            $("#UF").val(data.UF);
            $("#CEP").val(data.CEP);

            if (data == null) $("#rCEP").html(msgErro); //alert(msgErro);

        });
    }

}

function validarUF(valor, rootView) {
    var url = rootView + "/ValidarUF/";
    var valor = $(valor).val();
    $.get(url, { Uf: valor }, function (data) {
        $("#rUF").html(data);
    });
}

// MASCARA NOS CAMPOS
$(document).ready(function () {
    // $("#DtNasc").mask("(99) 9999-9999");
    $("#TelCelNum").mask("99999-9999");
    $("#TelResNum").mask("9999-9999");
    $("#DtNasc").mask("99/99/9999");
    $("#Rg").mask("9999999999");
    $("#Cpf").mask("999999999999");
    $("#Cnpj").mask("999999999999999");
    $("#CEP").mask("99999999");
});

function validarEmail(valor, rootView) {
    var url = rootView + "/ValidarEMAIL/";
    var valor = $(valor).val();
    $.get(url, { emailtx: valor }, function (data) {
        $("#rEmail").html(data);
    });
}

function validarDtNasc(valor, rootView) {
    var url = rootView + "/ValidarDtNasc/";
    var valor = $(valor).val();
    $.get(url, { dtnasc: valor }, function (data) {
        $("#rDtNasc").html(data);
    });
}

function ConfirmaAcao(acao) {
    return confirm("Deseja " + acao + "?");
}

function validarFormUsuario(rootView) {

    var formData = $("#formUsuario").serialize();

    $.ajax({
        type: "POST",
        data: formData,
        url: rootView + "/ValidarFormJSON",
        dataType: 'json',
        success: function (response) {

            //alert(response.msg + ' - ' + response.success);
            $("#rFormRetorno").html(response.msg);

            if (response.success) {
                alert("Operação realizada com sucesso!");
                window.location.replace("Index");
            } else {
                alert("Existe(m) campo(s) incorreto(s), verifique.");
            }

        },
        error: function (response) {
            alert("Ocorreu um erro durante o processo, tente novamente!\n retorno: " + response.status);
        }
    });
}

function ValidaConcordo(aviso) {

    var result = true;
    var msg = "";

    var countChecked = function () {

        if ($('input[name=contribuicao]:checked').length <= 0) {
            //alert("Para finalizar você deve selecionar a opção de contribuição.");
            msg += "Para finalizar você deve selecionar a opção de contribuição.\n";
            result = false;
        }

        if ($('input[name=pagamento]:checked').length <= 0) {
            //alert("Para finalizar você deve selecionar a opção de pagamento.");
            msg += "Para finalizar você deve selecionar a opção de pagamento.\n";
            result = false;
        }

    };

    countChecked();

    if (result) {
        var valor = $("#concordo");

        if (!valor.is(':checked')) {
            //alert("Para finalizar você deve clicar em '" + aviso + "'");
            msg += "Para finalizar você deve clicar em '" + aviso + "'\n";
            result = false;
        }
    }

    if (!result) {
        alert(msg);
    }
    
    return result;

}

function validarFormAfiliaEdita(rootView) {

    var formData = $("#formAfilia").serialize();

    $.ajax({
        type: "POST",
        data: formData,
        url: rootView + "/ValidarFormEditaJSON",
        dataType: 'json',
        // contentType: false,
        // processData: false,               
        success: function (response) {

            $("#rForm").html(response.msg);
            
            if (response.success) {
                window.location.replace("Dependentes");
            } else {
                alert(response.msg);
            }

        },
        error: function (response) { 
            alert("Ocorreu um erro durante o processo, tente novamente!\n retorno: " + response.status);
        }
    });
}