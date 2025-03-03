# Clerk ASP.net MVC Example 

This project is a 'template' that implements a basic auth middleware for Clerk.  The project should be used to see how the middleware can be used, but there are simplifying assumptions (such as an allows all CORS policy, and no HTTPS redirect) that make this project innapropriate to use in production.

## Setup

Install dependencies
```bash
$ dotnet install
```

Make sure the CLERK_SECRET_KEY [environment variable](https://clerk.com/docs/deployments/clerk-environment-variables#clerk-publishable-and-secret-keys) is set, ie:

```bash
export CLERK_SECRET_KEY=my_secret_key
```

## Usage

Run the server:
```bash
$ dotnet Run
```

From a Clerk frontend, use the `useSession` hook to retrieve the getToken() function:

```js
const session = useSession();
const getToken = session?.session?.getToken
```

Then, request the server:

```js
if (getToken) {
    // get the userId or None if the token is invalid
    const res = await fetch("http://localhost:5063/Home/ClerkJWTS", {
        headers: {
            "Authorization": `Bearer ${await getToken()}`
        }
    })
    console.log(await res.json()) // {userId: 'the_user_id_or_null'}

    // get gated data or a 401 Unauthorized if the token is not valid
    const res = await fetch("http://localhost:5063/Home/GatedData", {
        headers: {
            "Authorization": `Bearer ${await getToken()}`
        }
    })
    if (res.status === 401) {
        // token was invalid
    } else {
        console.log(await res.json()) // {foo: "bar"}
    }
}
```
