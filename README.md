# mach-assignment
Repository for Mach------'s assignment.

## Introduction

Hello all, thank you for the opportunity for me to develop this little challenge! All deliverable code can be found within the /src directory.

## Approach

### Logic

Doing a manual test with POSTMAN I was able to understand that the magic numbers were always different so therefore I would need to create a service that would query the magic numbers endpoint.

After querying the magic numbers endpoint we sum all the numbers and send a POST request to the same address with the sum and a callback URL.

The callback URL would have to be an URL I could pass so the remote client could send me the result so we would need to host this somewhere on the cloud/internet or port-forward from my network but I opted for the first option as a CI/CD pipeline was a requirement.

### Architecture

I decided to go for Azure App Services to host a Minimal Web API to run the process described above in a fast way. You can find it here:

https://mach-assignment-api-webapp.azurewebsites.net/

It returns true or false depending if the POST request's response has a successful status code. This doesn't determine if the actual result is correct, to determine/show the result with the callback URL I created a POST endpoint in the address below:

https://mach-assignment-api-webapp.azurewebsites.net/send

As I didn't know the remote client's request body format I had to adapt and use the HttpRequest's middleware to just read the body and output it to the logs, so I could see it.

### Pipeline

I first decided to opt for a simple docker build / docker push pipeline as I first wanted to use Azure App Services to pull the images automatically but I then decided to go the opposite way, for Github Actions to rule that decision. When a commit is pushed to the main branch it updates the application on Azure automatically, creating a continuous integration and deployment pipeline.

### Unit Tests

I added a "RUN dotnet test" to the Dockerfile as a step but I did not include any unit/integration tests with the project. I left the door open for the unit test possibility with abstraction/DI, applying DRY/SOLID principles.

To unit test our services I used Dependency Injection so we can in the future mock dependencies so we can arrange our unit tests accordingly.