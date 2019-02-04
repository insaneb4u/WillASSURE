
 $(document).ready(function(){

              $("#alert-basic").click(function(){
                  swal("Hello world!");
              });

              $("#alert-title").click(function(){
                  swal("Here's the title!", "...and here's the text!");
              });

     //success Alert Button


              $("#btnOTPCheckSUCCESS").click(function(){
                  swal("Verified!", "Your OTP has Been Verified!,", "success");
              });
     

              $("#btnDeleteTestatorData").click(function () {
                 swal("Deleted!", "Your Data has Been Deleted!,", "success");
             });

     //end

     //Failed Alert Button

     $("#btnOTPCheckFAILED").click(function(){
                  swal("Failed!", "Please Enter Correct OTP!,", "error");
              });


     //end

              $("#alert-info").click(function(){
                  swal("Information!", "You clicked the button!,", "info");
              });

              $("#alert-warning").click(function(){
                  swal("Warning!", "You clicked the button!,", "warning");
              });


              $("#confirm-btn-alert").click(function(){

                  swal({
                    title: "Are you sure?",
                    text: "Once deleted, you will not be able to recover this imaginary file!",
                    icon: "warning",
                    buttons: true,
                    dangerMode: true,
                  })
                  .then((willDelete) => {
                    if (willDelete) {
                      swal("Poof! Your imaginary file has been deleted!", {
                        icon: "success",
                      });
                    } else {
                      swal("Your imaginary file is safe!");
                    }
                  });

              });

          });