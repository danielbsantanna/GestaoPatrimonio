<template>
    <v-container>
        <v-row class="text-center">
            <v-card class="cartao-principal">
                <v-card-title>
                    <v-btn icon="mdi-arrow-left" elevation="0" @click="$router.go(-1)"></v-btn>
                    Cadastro de Grupos
                    <v-spacer></v-spacer>
                    <v-btn color="success" @click="adicionarItem" v-if="grupo.id > 0">
                        Adicionar Item
                    </v-btn>
                </v-card-title>
                <v-divider></v-divider>
                <v-card-text>
                    <v-row>
                        <v-col cols="12" sm="4">
                            <v-text-field v-model="grupo.nome" label="Nome" :disabled="permissao != 'Admin'">
                            </v-text-field>
                        </v-col>
                        <v-col cols="12" sm="2">
                            <v-text-field v-model="grupo.vidaUtil" label="Vida Útil (Meses)" type="number"
                                :disabled="permissao != 'Admin'">
                            </v-text-field>
                        </v-col>
                        <v-col cols="12" sm="4">
                            <v-text-field v-model="grupo.depreciacaoMensal" label="Depreciação Durante Vida Útil"
                                type="number" prefix="%" :disabled="permissao != 'Admin'">
                            </v-text-field>
                        </v-col>
                    </v-row>
                    <v-table v-if="grupo.id > 0">
                        <thead>
                            <tr>
                                <th class="text-left">
                                    Id
                                </th>
                                <th class="text-left">
                                    Descrição
                                </th>
                                <th class="text-left">
                                    Data Entrada
                                </th>
                                <th class="text-left">
                                    Valor
                                </th>
                                <th class="text-left">
                                    Quantidade
                                </th>
                                <th class="text-left">
                                    Valor Total
                                </th>
                                <th class="text-left">
                                    Valor Residual
                                </th>
                                <th class="text-left">
                                    Valor Depreciado
                                </th>
                                <th v-if="permissao == 'Admin'"></th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr v-for="item in notas" :key="item.id">
                                <td class="text-left">{{ item.id }}</td>
                                <td class="text-left">{{ item.descricao }}</td>
                                <td class="text-left">{{ retornaDataFormatada(item.dataEntrada) }}</td>
                                <td class="text-left">R$ {{ arredondar(item.valor, true) }}</td>
                                <td class="text-left">{{ item.quantidade }}</td>
                                <td class="text-left">R$ {{ arredondar(item.valor * item.quantidade, true) }}</td>
                                <td class="text-left">R$ {{ arredondar((retornaDepreciacao(item) == 0 ? 0 : item.valor *
                                        item.quantidade) - retornaDepreciacao(item), true)
                                }}
                                </td>
                                <td class="text-left">R$ {{ arredondar(retornaDepreciacao(item) == 0 ? item.valor *
                                        item.quantidade : retornaDepreciacao(item), true)
                                }}</td>
                                <td class="text-right" v-if="permissao == 'Admin'">
                                    <v-btn color="warning" size="x-small" icon="mdi-pencil" @click="editarItem(item)">
                                    </v-btn>
                                </td>
                            </tr>
                            <tr>
                                <td class="text-left"></td>
                                <td class="text-left"></td>
                                <td class="text-left"></td>
                                <td class="text-left">
                                    <h3>Total:</h3>
                                </td>
                                <td class="text-left">{{ totais.quantidade }}</td>
                                <td class="text-left">R$ {{ arredondar(totais.valor, true) }}</td>
                                <td class="text-left">R$ {{ arredondar(totais.valorResidual, true) }}</td>
                                <td class="text-left">R$ {{ arredondar(totais.valor - totais.valorResidual, true) }}
                                </td>
                            </tr>
                        </tbody>
                    </v-table>
                </v-card-text>
                <v-divider></v-divider>
                <v-card-actions class='cartao-login-actions'>
                    <v-btn @click="botaoSalvar">{{ grupo.id != 0 ? 'Salvar' : 'Cadastrar' }}</v-btn>
                </v-card-actions>
            </v-card>
        </v-row>
        <v-snackbar v-model="snackbar" timeout="5000" :color="snackbarSucesso ? 'success' : 'red-darken-2'">
            {{ textSnackbar }}
            <template v-slot:actions>
                <v-btn icon="mdi-close" @click="snackbar = false"></v-btn>
            </template>
        </v-snackbar>
        <v-dialog v-model="modalItem" persistent>
            <v-card>
                <v-card-title>
                    Adicionar Item
                    <v-spacer></v-spacer>
                    <v-btn icon="mdi-close" @click="modalItem = false"></v-btn>
                </v-card-title>
                <v-divider></v-divider>
                <v-card-text>
                    <v-row>
                        <v-col cols="12">
                            <v-text-field v-model="novaNota.descricao" label="Descrição">
                            </v-text-field>
                        </v-col>
                        <v-col cols="12" sm="4">
                            <v-text-field v-model="novaNota.valor" label="Valor" type="number">
                            </v-text-field>
                        </v-col>
                        <v-col cols="12" sm="4">
                            <v-text-field v-model="novaNota.quantidade" label="Quantidade" type="number">
                            </v-text-field>
                        </v-col>
                        <!-- <v-col cols="12" sm="4">
                            <v-text-field v-model="novaNota.dataEmissao" label="Date Emissão" type="date">
                            </v-text-field>
                        </v-col> -->
                        <v-col cols="12" sm="4">
                            <v-text-field v-model="novaNota.dataEntrada" label="Data Entrada" type="date">
                            </v-text-field>
                        </v-col>
                    </v-row>
                </v-card-text>
                <v-card-actions class='cartao-login-actions'>
                    <v-btn @click="salvarItem">Salvar</v-btn>
                </v-card-actions>
            </v-card>
        </v-dialog>
    </v-container>
</template>

<script>

import api from "../../api"
import moment from 'moment'


export default {

    data: () => ({
        grupo: {
            id: 0,
            nome: '',
            depreciacaoMensal: 0,
            vidaUtil: 0
        },
        novaNota: {
            id: 0,
            dataEmissao: null,
            dataEntrada: null,
            descricao: '',
            grupoId: 0,
            quantidade: 0,
            valor: 0,
        },
        textSnackbar: "",
        snackbar: false,
        snackbarSucesso: false,
        notas: [],
        modalItem: false,
        teste: new Date(),
        totais: {
            quantidade: 0,
            valor: 0,
            valorResidual: 0,
        },
        permissao: "Usuario",
        editandoNota: false,
    }),
    methods: {
        botaoSalvar() {
            if (this.grupo.id == 0) {
                api
                    .post("/grupo/Inserir", this.grupo)
                    .then((res) => {
                        this.textSnackbar = res;
                        this.snackbar = true;
                        this.snackbarSucesso = true;
                        this.grupo.nome = '';
                        this.grupo.depreciacaoMensal = 0;
                        this.grupo.vidaUtil = 0;
                    })
                    .catch((error) => {
                        this.textSnackbar = error ? error : 'Ocorreu um erro interno do sistema.';
                        this.snackbar = true;
                        this.snackbarSucesso = false;
                    });
            } else {
                this.grupo.id = parseInt(this.grupo.id)
                api
                    .put("/grupo/Editar", this.grupo)
                    .then((res) => {
                        this.textSnackbar = res;
                        this.snackbar = true;
                        this.snackbarSucesso = true;
                    })
                    .catch((error) => {
                        this.textSnackbar = error ? error : 'Ocorreu um erro interno do sistema.';
                        this.snackbar = true;
                        this.snackbarSucesso = false;
                    });
            }
        },
        consultarGrupo() {
            api.get("grupo/consultar", {
                params: {
                    id: this.grupo.id
                }
            }).then(x => {
                this.grupo.nome = x.nome
                this.grupo.depreciacaoMensal = x.depreciacaoMensal
                this.grupo.vidaUtil = x.vidaUtil
                this.listarNotas()
            }).catch((error) => {
                this.textSnackbar = error ? error : 'Ocorreu um erro interno do sistema.'
                this.snackbar = true
                this.snackbarSucesso = false
                this.grupo.id = 0
            })
        },
        listarNotas() {
            if (this.grupo.id != 0) {
                api.get("nota/listar", {
                    params: {
                        grupo: this.grupo.id
                    }
                }).then(x => {
                    this.notas = x
                    this.calcularTotais()
                }).catch((error) => {
                    this.textSnackbar = error ? error : 'Ocorreu um erro interno do sistema.'
                    this.snackbar = true
                    this.snackbarSucesso = false
                })
            }
        },
        adicionarItem() {
            this.editandoNota = false
            this.novaNota.grupoId = parseInt(this.grupo.id)
            this.novaNota.dataEmissao = moment(new Date()).format("YYYY-MM-DD")
            this.novaNota.dataEntrada = moment(new Date()).format("YYYY-MM-DD")
            this.novaNota.descricao = ''
            this.novaNota.valor = 0
            this.novaNota.quantidade = 0
            this.novaNota.id = 0
            this.modalItem = true
        },
        salvarItem() {
            this.novaNota.dataEmissao = this.novaNota.dataEntrada
            if (this.editandoNota) {
                api
                    .put("/nota/Editar", this.novaNota)
                    .then((res) => {
                        this.textSnackbar = res
                        this.snackbar = true
                        this.snackbarSucesso = true
                        this.consultarGrupo()
                        this.modalItem = false
                    })
                    .catch((error) => {
                        this.textSnackbar = this.textSnackbar = error ? error : 'Ocorreu um erro interno do sistema.'
                        this.snackbarSucesso = false
                        this.snackbar = true
                    })
            } else {
                api
                    .post("/nota/Inserir", this.novaNota)
                    .then((res) => {
                        this.textSnackbar = res
                        this.snackbar = true
                        this.snackbarSucesso = true
                        this.consultarGrupo()
                        this.modalItem = false
                    })
                    .catch((error) => {
                        this.textSnackbar = this.textSnackbar = error ? error : 'Ocorreu um erro interno do sistema.'
                        this.snackbarSucesso = false
                        this.snackbar = true
                    })
            }

        },
        retornaDataFormatada(date) {
            if (date) {
                return moment(date).format("DD/MM/YYYY");
            }
        },
        retornaDepreciacao(item) {
            var diferenca = moment(new Date()).diff(moment(item.dataEmissao), 'months', true)
            diferenca = parseInt(diferenca.toString().split('.')[0])
            if (diferenca == 0)
                return 0
            if (diferenca > this.grupo.vidaUtil)
                return item.valor * item.quantidade
            var depreciacao = this.grupo.depreciacaoMensal / this.grupo.vidaUtil
            var depreciacaoMensal = ((100 + depreciacao) / 100) - 1
            var valorMensal = (item.valor * item.quantidade) * depreciacaoMensal
            return this.arredondar(valorMensal * diferenca)
        },
        calcularTotais() {
            this.totais.quantidade = 0
            this.totais.valor = 0
            this.totais.valorResidual = 0
            this.notas.forEach(x => {
                this.totais.quantidade += x.quantidade
                this.totais.valor += x.valor * x.quantidade
                this.totais.valorResidual += (x.valor * x.quantidade) - this.retornaDepreciacao(x)
            })
        },
        arredondar(valor, formatado = false) {
            var round = Math.round(valor * 100) / 100
            if (formatado)
                return round.toFixed(2).replaceAll('.', ',').replace(/\B(?=(\d{3})+(?!\d))/g, ".")
            return round
        },
        editarItem(item) {
            this.editandoNota = true
            this.novaNota.id = item.id
            this.novaNota.grupoId = item.grupoId
            this.novaNota.dataEmissao = moment(item.dataEmissao).format("YYYY-MM-DD")
            this.novaNota.dataEntrada = moment(item.dataEntrada).format("YYYY-MM-DD")
            this.novaNota.descricao = item.descricao
            this.novaNota.valor = item.valor
            this.novaNota.quantidade = item.quantidade
            this.modalItem = true
        }
    },
    mounted() {
        this.grupo.id = this.$route.params.id
        if (this.grupo.id != 0) {
            this.consultarGrupo()
        }

        this.permissao = localStorage.getItem("permissao")
    }
}
</script>
