<template>
    <v-container>
        <v-row class="text-center">
            <v-card class="cartao-principal">
                <v-card-title>
                    <v-btn icon="mdi-arrow-left" elevation="0" @click="$router.go(-1)"></v-btn>
                    Cadastro de Notas
                </v-card-title>
                <v-divider></v-divider>
                <v-card-text>
                    <v-row>
                        <v-col cols="12">
                            <v-text-field v-model="nota.descricao" label="Descrição">
                            </v-text-field>
                        </v-col>
                        <v-col cols="12" sm="4">
                            <v-select :items="selectGrupos" v-model="nota.grupoId" label="Grupo"></v-select>
                        </v-col>
                        <v-col cols="12" sm="4">
                            <v-text-field v-model="nota.valor" label="Valor" type="number">
                            </v-text-field>
                        </v-col>
                        <v-col cols="12" sm="4">
                            <v-text-field v-model="nota.quantidade" label="Quantidade" type="number">
                            </v-text-field>
                        </v-col>
                        <v-col cols="12" sm="4">
                            <v-text-field v-model="nota.dataEmissao" label="Date Emissão">
                            </v-text-field>
                        </v-col>
                        <v-col cols="12" sm="4">
                            <v-text-field v-model="nota.dataEntrada" label="Data Entrada">
                            </v-text-field>
                        </v-col>
                    </v-row>
                </v-card-text>
                <v-divider></v-divider>
                <v-card-actions class='cartao-login-actions'>
                    <v-btn @click="criarNota">Cadastrar</v-btn>
                </v-card-actions>
            </v-card>
        </v-row>
        <v-snackbar v-model="snackbar" timeout="5000" :color="snackbarSucesso ? 'success' : 'red-darken-2'">
            {{ textSnackbar }}
            <template v-slot:actions>
                <v-btn icon="mdi-close" @click="snackbar = false"></v-btn>
            </template>
        </v-snackbar>
    </v-container>
</template>

<script>

import { onMounted } from "vue";
import api from "../../api";

export default {

    data: () => ({
        nota: {
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
        grupos: []
    }),
    methods: {
        criarNota() {
            api
                .post("/nota/Inserir", this.nota)
                .then((res) => {
                    this.textSnackbar = res
                    this.snackbar = true
                    this.snackbarSucesso = false
                })
                .catch((error) => {
                    this.textSnackbar = this.textSnackbar = error ? error : 'Ocorreu um erro interno do sistema.'
                    this.snackbarSucesso = false
                    this.snackbar = true
                })
        },
        retornarGrupos() {
            api
                .get("/grupo/Listar"
                )
                .then((res) => {
                    this.grupos = res
                })
                .catch((error) => {
                    this.textSnackbar = this.textSnackbar = error ? error : 'Ocorreu um erro interno do sistema.'
                    this.snackbarSucesso = false
                    this.snackbar = true
                })
        },

    },
    mounted() {
        this.retornarGrupos()
    },
    computed: {
        selectGrupos() {
            var newArray = []
            this.grupos.forEach(x => {
                newArray.push({ title: x.nome, value: x.id })
            })
            return newArray
        }
    }
}
</script>
