window.SwalConfirm = async function (options) {
    const result = await Swal.fire(options);
    return result.isConfirmed;
};
