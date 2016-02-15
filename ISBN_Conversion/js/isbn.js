$(document).ready(function () {
    $('#edit-submit').click(function (e) {
        //stop the submit button
        e.preventDefault();

        //remove any - 
        var isbn10 = $('#edit-isbn10').val().replace(/-/gi, "");
        isbn10 = $.trim(isbn10);

        //make sure it's at least 10 characters and all numbers
        if (isbn10 == '' || isbn10.length < 10 || !/^[0-9xX]+$/.test(isbn10)) {
            alert('The isbn number is incorrect. Please check it and try again.');
            return false;
        }

        var converted = convertISBN(isbn10);
        $('#isbn13_conversion').text(converted);
        return true;
    });

});

function convertISBN(isbnnum) {

    var isbnlen = isbnnum.length;
    var total = 0;

    // convert a 10-digit ISBN
    if (isbnlen == 10) {

        for (var x = 0; x < 9; x++) {
            total = total + (isbnnum.charAt(x) * (10 - x));
        }

        // check digit
        z = isbnnum.charAt(9);
        if (z == "X") { z = 10; }

        // validate ISBN
        if ((total + z * 1) % 11 != 0) {
            z = (11 - (total % 11)) % 11;
            if (z == 10) { z = "X"; }
            alert("This 10-digit ISBN is invalid." + "\n" +
                "The check digit should be " + z + ".");

            return 'invalid isbn';
        }
        else {
            // convert to a 13-digit ISBN
            isbnnum = "978" + isbnnum.substring(0, 9);
            total = 0;
            for (var x = 0; x < 12; x++) {
                if ((x % 2) == 0) { y = 1; }
                else { y = 3; }
                total = total + (isbnnum.charAt(x) * y);
            }
            z = (10 - (total % 10)) % 10;
        }
    }

    // convert a 13-digit ISBN
    else { 
        for (var x = 0; x < 12; x++) {
            if ((x % 2) == 0) { y = 1; }
            else { y = 3; }
            total = total + (isbnnum.charAt(x) * y);
        }

        // check digit
        z = isbnnum.charAt(12);

        // validate ISBN		
        if ((10 - (total % 10)) % 10 != z) { 
            z = (10 - (total % 10)) % 10;
            alert("This 13-digit ISBN is invalid." + "\n" +
                "The check digit should be " + z + ".");
            return 'invalid isbn';
        }
        else {
            // convert the 13-digit ISBN to a 10-digit ISBN
            if ((isbnnum.substring(0, 3) != "978")) {
                alert("This 13-digit ISBN does not begin with \"978\"" + "\n" +
                    "It cannot be converted to a 10-digit ISBN.");

                return 'invalid isbn';
            }
            else {
                isbnnum = isbnnum.substring(3, 12);
                total = 0;
                for (var x = 0; x < 9; x++) {
                    total = total + (isbnnum.charAt(x) * (10 - x));
                }
                z = (11 - (total % 11)) % 11;
                if (z == 10) { z = "X"; }
            }
        }
    }

    return isbnnum + z;

}