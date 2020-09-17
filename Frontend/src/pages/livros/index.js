import React, { useContext, useEffect, useState } from "react";
import api from "../../api";
import { store } from '../../store';

export default function Livros() {
	const modalFechada = { livro: undefined, visivel: false };
	const globalState = useContext(store);
	const [livros, setLivros] = useState([]);
	const [modal, setModal] = useState(modalFechada);

	useEffect(() => {
		api.get(`/api/livro`, globalState.state.usuario)
			.then(json => json.json())
			.then(data => {
				setLivros(data);
			});
	}, []);

	function onDetalhe(e, livro) {
		const _modal = { livro, visivel: true, close: () => { console.log("OK"); setModal(modalFechada); } };
		setModal(_modal);
	}

	return <>
		<table className="w-full text-md bg-white shadow-md rounded mb-4">
			<tbody>
				<tr className="border-b">
					<th className="text-left p-3 px-5">Nome</th>
					<th className="text-left p-3 px-5">Ano</th>
					<th className="text-left p-3 px-5">Autor</th>
					<th></th>
				</tr>
				{livros.map((livro, i) =>
					<tr className={"border-b hover:bg-orange-100 cursor-pointer " + (i % 2 === 0 ? "bg-gray-100" : "")} key={i}>
						<td className="p-3 px-5">{livro.nome}</td>
						<td className="p-3 px-5">{livro.ano}</td>
						<td className="p-3 px-5">{livro.autor}</td>
						<td className="p-3 px-5 flex justify-end">
							<button type="button" className="mr-3 text-sm bg-blue-500 hover:bg-blue-700 text-white py-1 px-2 rounded focus:outline-none focus:shadow-outline" onClick={(e) => onDetalhe(e, livro)}>Detalhes</button>
							{/* <button type="button" className="text-sm bg-red-500 hover:bg-red-700 text-white py-1 px-2 rounded focus:outline-none focus:shadow-outline">Delete</button> */}
						</td>
					</tr>
				)}
			</tbody>
		</table>
		{modal.visivel && <ModalDetalhe livro={modal.livro} close={modal.close} />}
	</>;
}

function ModalDetalhe(props) {
	return <div className="flex items-center justify-center fixed left-0 bottom-0 w-full h-full bg-gray-800">
		<div className="bg-white rounded-lg w-1/2">
			<div className="flex flex-col items-start p-4">
				<div className="flex items-center w-full mb-2">
					<div className="text-gray-900 font-medium text-lg">{props.livro.nome} / {props.livro.ano}</div>
					<svg onClick={props.close} className="ml-auto fill-current text-gray-700 w-6 h-6 cursor-pointer" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 18 18">
						<path d="M14.53 4.53l-1.06-1.06L9 7.94 4.53 3.47 3.47 4.53 7.94 9l-4.47 4.47 1.06 1.06L9 10.06l4.47 4.47 1.06-1.06L10.06 9z" />
					</svg>
				</div>
				<hr />
				<p>Nome: {props.livro.nome}</p>
				<p>Ano: {props.livro.ano}</p>
				<p>Autor: {props.livro.autor}</p>
				<p>Volume: {props.livro.volume}</p>
				<p>Valor: {props.livro.valorAluguel.toFixed(2)}</p>
				<hr />
				<div className="ml-auto">
					<button onClick={props.close} className="bg-transparent hover:bg-gray-500 text-blue-700 font-semibold hover:text-white py-2 px-4 border border-blue-500 hover:border-transparent rounded">
						Fechar
        </button>
				</div>
			</div>
		</div>
	</div>;
}