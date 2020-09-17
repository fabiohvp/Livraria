import React, { useContext } from 'react';
import { store } from '../store.js';
import { Link, useHistory } from "react-router-dom";

export default function Header() {
	const globalState = useContext(store);
	const { dispatch } = globalState;
	const history = useHistory();

	function onLogout() {
		dispatch({ type: 'logout' });
		history.push("/");
	}

	return <header className="text-gray-100 bg-gray-900 body-font shadow w-full">
		<div className="flex p-5 items-center">
			<nav className="items-center text-base w-full">
				<Link to="/" className="text-2xl no-underline text-grey-darkest hover:text-blue-dark font-sans font-bold">Livraria </Link>
				<Link to="/livros" className="ml-5 hover:text-gray-400 cursor-pointer border-b border-transparent hover:border-indigo-600">Livros</Link>
			</nav>
			{globalState.state.usuario &&
				<div className="pull-right">
					<button onClick={onLogout} className="bg-indigo-700 hover:bg-indigo-500 text-white ml-4 py-2 px-3 rounded-lg">Sair</button>
				</div>
			}
		</div>
	</header>;
}