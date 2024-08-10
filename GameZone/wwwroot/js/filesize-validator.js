$.validator.addMethod('fileSize', function (value, element, params) {
    return this.Optional(element) || element.files[0].size <= params;
});