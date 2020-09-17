const host = "https://localhost:44391"

export async function _get(url, usuario, _options) {
	const options = {
		method: "get",
		headers: {
			'Accept': 'application/json',
			'Content-Type': 'application/json',
			'Authorization': usuario ? 'Bearer ' + usuario.access_token : undefined,
		},
		..._options,
	}
	return await fetch(`${host}${url}`, options);
}

export async function _post(url, values, usuario, _options) {
	const options = {
		method: "post",
		headers: {
			'Accept': 'application/json',
			'Content-Type': 'application/json',
			'Authorization': usuario ? 'Bearer ' + usuario.access_token : undefined,
		},
		body: JSON.stringify(values),
		..._options,
	}
	console.log(options);
	return await fetch(`${host}${url}`, options);
}

export async function _auth(url, { email, senha }, _options) {
	const body = new URLSearchParams({
		'grant_type': 'password',
		'username': email,
		'password': senha
	});

	const options = {
		method: "post",
		headers: {
			'Accept': 'application/json',
			'Content-Type': 'application/x-www-form-urlencoded',
		},
		body: body.toString(),
		..._options,
	}
	return await fetch(`${host}${url}`, options);
}

export default {
	auth: _auth,
	get: _get,
	post: _post
}

