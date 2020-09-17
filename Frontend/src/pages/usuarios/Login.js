import React, { useContext, useState } from 'react';
import { useHistory } from "react-router-dom";
import api from "../../api";
import { store } from '../../store';

export default function Login() {
	const globalState = useContext(store);
	const { dispatch } = globalState;
	const history = useHistory();

	const [input, setInput] = useState({ email: 'admin@livraria.com', senha: '123456' });
	const [error, setError] = useState("");
	//let error = "";

	function onInputChange(e, field) {
		const form = { ...input };
		form[field] = e.target.value;
		setInput(form);
	}

	async function onLogin() {
		const res = await api.auth('/token', input);
		const data = await res.json();

		if (data.error_description) {
			setError(data.error_description);
		}
		else {
			const usuario = {
				...JSON.parse(data.usuario),
				'.expires': data['.expires'],
				'.issued': data['.issued'],
				access_token: data.access_token,
				expires_in: data.expires_in,
				token_type: data.token_type
			};
			dispatch({ type: 'login', payload: usuario });
			history.push("/livros");
		}
	}

	return <form>
		<div className="lg:w-2/4 m-auto px-8 pt-6 pb-8 mb-4 flex flex-col">
			<div className="mb-4">
				<label className="block text-gray-600 text-sm font-bold mb-2">
					Login
		</label>
				<input value={input.email}
					onChange={(e) => onInputChange(e, 'email')}
					className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-600" type="text" placeholder="E-mail" />
			</div>
			<div className="mb-6">
				<label className="block text-gray-600 text-sm font-bold mb-2">
					Senha
		</label>
				<input value={input.senha}
					onChange={(e) => onInputChange(e, 'senha')}
					className="shadow appearance-none border border-red rounded w-full py-2 px-3 text-gray-600 mb-3" type="password" placeholder="**********" />
			</div>
			<div className="flex items-center">
				<button className="bg-blue-600 hover:bg-blue-900 text-white font-bold py-2 px-4 rounded" type="button" onClick={onLogin}>
					Entrar
		</button>
				{error && <span className="ml-4 text-red-600 italic">{error}</span>}
			</div>
		</div>
	</form>;
}