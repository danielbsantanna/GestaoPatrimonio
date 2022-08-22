<template>

  <v-container>
    <v-row>
      <v-col>
        <v-card class="cartao-principal elevation-10">

          <v-card-title class="cartao-login-titulo">
            Demonstrativo de Patrimônio
          </v-card-title>
          <v-card-text>
            <v-table>
              <thead>
                <tr>
                  <th class="text-left">
                    Grupo
                  </th>
                  <th class="text-left">
                    Valor
                  </th>
                  <th class="text-left">
                    Valor Residual
                  </th>
                  <th class="text-left">
                    Valor Depreciado
                  </th>
                </tr>
              </thead>
              <tbody>
                <tr v-for="item in itens" :key="item.id">
                  <td class="text-left">{{ item.grupo }}</td>
                  <td class="text-left">R$ {{ arredondar(item.valor, true) }}</td>
                  <td class="text-left">R$ {{ arredondar(item.valorResidual, true) }}</td>
                  <td class="text-left">R$ {{ arredondar(item.valorDepreciado, true) }}</td>

                </tr>
                <tr>
                  <td class="text-left" style="font-weight: bold">
                    Total
                  </td>
                  <td class="text-left" style="font-weight: bold">R$ {{ arredondar(totais.valor, true) }}</td>
                  <td class="text-left" style="font-weight: bold">R$ {{ arredondar(totais.valorResidual, true) }}</td>
                  <td class="text-left" style="font-weight: bold">R$ {{ arredondar(totais.valorDepreciado, true) }}
                  </td>
                </tr>
              </tbody>
            </v-table>
          </v-card-text>
        </v-card>
      </v-col>
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
import api from "../../api"

export default {
  data: () => ({
    itens: [],
    textSnackbar: "",
    snackbar: false,
    snackbarSucesso: false,
    totais: {
      valorDepreciado: 0,
      valor: 0,
      valorResidual: 0,
    },
  }),
  methods: {
    arredondar(valor, formatado = false) {
      var round = Math.round(valor * 100) / 100
      if (formatado)
        return round.toFixed(2).replaceAll('.', ',').replace(/\B(?=(\d{3})+(?!\d))/g, ".")
      return round
    },
    calcularTotais() {
      this.totais.valorDepreciado = 0
      this.totais.valor = 0
      this.totais.valorResidual = 0
      this.itens.forEach(x => {
        this.totais.valorDepreciado += x.valorDepreciado
        this.totais.valor += x.valor
        this.totais.valorResidual += x.valorResidual
      })
    },
  },
  mounted() {
    api.get("nota/DemostrativoPatrimonio", {
      params: {
      }
    }).then(x => {
      this.itens = x
      this.calcularTotais()
    }).catch((error) => {
      this.textSnackbar = error ? error : 'Ocorreu um erro interno do sistema.'
      this.snackbar = true
      this.snackbarSucesso = false
    })
  }
}
</script>
